using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;

using FMath.Linear.Generic;

namespace FMath.Linear.Static
{
    /// <summary>
    /// Static class for handling <see cref="IVector"/> instances.
    /// </summary>
    public static class Vector
    {
        /// <summary>
        /// The salting prime used in the vector hashing function.
        /// </summary>
        public const int C_HashSaltPrime = 409;

        /// <summary>
        /// Checks whether the specified index is defined in the vector.
        /// </summary>
        /// <param name="AVector">The vector.</param>
        /// <param name="AIndex">The index.</param>
        /// <returns><c>true</c> if the index is defined, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when AVector is null.</exception>
        [Pure]
        public static bool IsDefined(
            this IVector AVector,
            int AIndex)
        {
            if (AVector == null)
                throw new ArgumentNullException("AVector");

            return AIndex >= 0 && AIndex < AVector.Size;
        }

        #region Equality
        /// <summary>
        /// Checks whether two vectors are equal by comparing their elements.
        /// </summary>
        /// <param name="ALeft">The left vector.</param>
        /// <param name="ARight">The right vector.</param>
        /// <returns><c>true</c> if they are equal, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static bool AreEqual(IVector ALeft, IVector ARight)
        {
            return Vector.AreEqual(ALeft, ARight, Object.Equals);
        }
        /// <summary>
        /// Checks whether two vectors are equal by comparing their elements.
        /// </summary>
        /// <param name="ALeft">The left vector.</param>
        /// <param name="ARight">The right vector.</param>
        /// <param name="AEquator">The element equator delegate.</param>
        /// <returns>
        ///   <c>true</c> if they are equal, <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static bool AreEqual(
            IVector ALeft,
            IVector ARight,
            Func<object, object, bool> AEquator)
        {
            if (ALeft == null)
                throw new ArgumentNullException("ALeft");
            if (ARight == null)
                throw new ArgumentNullException("ARight");
            if (AEquator == null)
                throw new ArgumentNullException("AEquator");

            if (ALeft.Size != ARight.Size)
                return false;

            for (int I = 0; I < ALeft.Size; I++)
                if (!AEquator(ALeft.Get(I), ARight.Get(I)))
                    return false;

            return true;
        }
        /// <summary>
        /// Checks whether two strongly typed vectors are equal by comparing their elements.
        /// </summary>
        /// <typeparam name="TLeft">The element type of the left vector.</typeparam>
        /// <typeparam name="TRight">The element type of the right vector.</typeparam>
        /// <param name="ALeft">The left vector.</param>
        /// <param name="ARight">The right vector.</param>
        /// <param name="AEquator">The element equator delegate.</param>
        /// <returns>
        ///   <c>true</c> if they are equal, <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static bool AreEqual<TLeft, TRight>(
            IVector<TLeft> ALeft,
            IVector<TRight> ARight,
            Func<TLeft, TRight, bool> AEquator)
        {
            if (AEquator == null)
                throw new ArgumentNullException("AEquator");

            return Vector.AreEqual((IVector)ALeft, ARight, (AL, AR) => AEquator((TLeft)AL, (TRight)AR));
        }
        /// <summary>
        /// Checks whether two strongly typed vectors are equal by comparing their elements.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="ALeft">The left vector.</param>
        /// <param name="ARight">The right vector.</param>
        /// <param name="AComparer">The element comparer, or <c>null</c> to use the default comparer.</param>
        /// <returns>
        ///   <c>true</c> if they are equal, <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static bool AreEqual<TData>(
            IVector<TData> ALeft,
            IVector<TData> ARight,
            IEqualityComparer<TData> AComparer = null)
        {
            if (AComparer == null)
                AComparer = EqualityComparer<TData>.Default;

            return Vector.AreEqual(ALeft, ARight, AComparer.Equals);
        }
        #endregion

        #region Hashing
        /// <summary>
        /// Hashes the specified vector.
        /// </summary>
        /// <param name="AVector">A vector.</param>
        /// <returns>An element-wise computed hash that also contains the size of the vector.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static int Hash(IVector AVector)
        {
            return Vector.Hash(AVector, EqualityComparer<object>.Default.GetHashCode);
        }
        /// <summary>
        /// Hashes the specified vector.
        /// </summary>
        /// <param name="AVector">The vector.</param>
        /// <param name="AHasher">The element hashing delegate.</param>
        /// <returns>An element-wise computed hash that also contains the size of the vector.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static int Hash(
            IVector AVector,
            Func<object, int> AHasher)
        {
            if (AVector == null)
                throw new ArgumentNullException("AVector");
            if (AHasher == null)
                throw new ArgumentNullException("AHasher");

            int iHash = AVector.Size;
            for (int I = 0; I < AVector.Size; I++)
                iHash = unchecked (iHash*Vector.C_HashSaltPrime + AHasher(AVector.Get(I)));

            return iHash;
        }
        /// <summary>
        /// Hashes the specified strongly typed vector.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="AVector">The vector.</param>
        /// <param name="AHasher">The element hashing delegate.</param>
        /// <returns>An element-wise computed hash that also contains the size of the vector.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static int Hash<TData>(
            IVector<TData> AVector,
            Func<TData, int> AHasher)
        {
            if (AHasher == null)
                throw new ArgumentNullException("AHasher");

            return Vector.Hash((IVector)AVector, AElement => AHasher((TData)AElement));
        }
        /// <summary>
        /// Hashes the specified strongly typed vector.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="AVector">The vector.</param>
        /// <param name="AComparer">The element comparer, or null to use the default comparer.</param>
        /// <returns>An element-wise computed hash that also contains the size of the vector.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static int Hash<TData>(
            IVector<TData> AVector,
            IEqualityComparer<TData> AComparer = null)
        {
            if (AComparer == null)
                AComparer = EqualityComparer<TData>.Default;

            return Vector.Hash(AVector, AComparer.GetHashCode);
        }
        #endregion

        #region Copying
        /// <summary>
        /// Copies data from one vector into another.
        /// </summary>
        /// <param name="ASource">The source vector.</param>
        /// <param name="ASourceOffset">The source offset index.</param>
        /// <param name="ATarget">The target vector.</param>
        /// <param name="ATargetOffset">The target offset index.</param>
        /// <param name="ACount">The number of elements to copy.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when one of the vectors is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the operation would exceed the vector bounds.</exception>
        public static void Copy(
            IVector ASource,
            int ASourceOffset,
            IMutableVector ATarget,
            int ATargetOffset,
            int ACount)
        {
            if (ASource == null)
                throw new ArgumentNullException("ASource");
            if (!ASource.IsDefined(ASourceOffset))
                throw new ArgumentOutOfRangeException("ASourceOffset");

            if (ATarget == null)
                throw new ArgumentNullException("ATarget");
            if (!ATarget.IsDefined(ATargetOffset))
                throw new ArgumentOutOfRangeException("ATargetOffset");

            if (ACount == 0)
                return;
            if (ACount < 0)
                throw new ArgumentOutOfRangeException("ACount");
            if (!ASource.IsDefined(ASourceOffset + ACount - 1))
                throw new ArgumentOutOfRangeException("ACount");
            if (!ATarget.IsDefined(ATargetOffset + ACount - 1))
                throw new ArgumentOutOfRangeException("ACount");

            for (int I = 0; I < ACount; I++)
                ATarget.Set(ATargetOffset + I, ASource.Get(ASourceOffset + I));
        }
        /// <summary>
        /// Copies all data from one vector into another.
        /// </summary>
        /// <param name="ASource">The source vector.</param>
        /// <param name="ATarget">The target vector.</param>
        public static void Copy(
            IVector ASource,
            IMutableVector ATarget)
        {
            Vector.Copy(ASource, 0, ATarget, 0, ASource.Size);
        }
        #endregion

        #region String formatting
        /// <summary>
        /// Formats an element of the vector.
        /// </summary>
        /// <param name="AVector">The vector.</param>
        /// <param name="AIndex">The index of the element.</param>
        /// <param name="AElementFormat">The element format.</param>
        /// <param name="AFormatProvider">The format provider.</param>
        /// <returns>The formatted element.</returns>
        /// <exception cref="ArgumentNullException">Thrown when AVector is null.</exception>
        [Pure]
        public static string FormatElement(
            IVector AVector,
            int AIndex,
            string AElementFormat = null,
            IFormatProvider AFormatProvider = null)
        {
            if (AVector == null)
                throw new ArgumentNullException("AVector");

            return AVector.Get(AIndex).SafeFormat(AElementFormat, AFormatProvider);
        }
        /// <summary>
        /// Linearizes the specified vector.
        /// </summary>
        /// <param name="AVector">The vector.</param>
        /// <param name="AElementFormat">The element format.</param>
        /// <param name="AFormatProvider">The format provider.</param>
        /// <returns>A linear representation of the vector.</returns>
        /// <exception cref="ArgumentNullException">Thrown when AVector is null.</exception>
        [Pure]
        public static string FormatLinear(
            IVector AVector,
            string AElementFormat = null,
            IFormatProvider AFormatProvider = null)
        {
            if (AVector == null)
                throw new ArgumentNullException("AVector");

            StringBuilder sbResult = new StringBuilder();

            sbResult.Append("[");

            for (int I = 0; I < AVector.Size; I++)
            {
                if (I != 0)
                    sbResult.Append(", ");

                sbResult.Append(Vector.FormatElement(AVector, I, AElementFormat, AFormatProvider));
            }

            sbResult.Append("]");

            return sbResult.ToString();
        }
        /// <summary>
        /// Formats the specified vector.
        /// </summary>
        /// <param name="AVector">The vector.</param>
        /// <param name="AFormatMode">The format mode.</param>
        /// <param name="AElementFormat">The element format.</param>
        /// <param name="AFormatProvider">The format provider.</param>
        /// <returns>The resulting string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when AVector is null.</exception>
        /// <remarks>
        /// The following format options are supported:
        /// <list type="table">
        ///     <listheader>
        ///         <term>AFormatMode</term>
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item>
        ///         <term>null, "G"(CD), "L"(CD), "linear"(ci)</term>
        ///         <description>Linearizes the vector.</description>
        ///     </item>
        ///     <item>
        ///         <term>"T"(CD), "type"(ci)</term>
        ///         <description>Outputs the element type and the size of the vector.</description>
        ///     </item>
        ///     <item>
        ///         <term>'a'-'z'(CD)</term>
        ///         <description>Outputs the specified named element (converted to zero-based index starting with a=0).</description>
        ///     </item> 
        ///     <item>
        ///         <term>&lt;number&gt;</term>
        ///         <description>Outputs the specified indexed element.</description>
        ///     </item>   
        /// </list>
        /// </remarks>
        [Pure]
        public static string Format(
            IVector AVector,
            string AFormatMode = null,
            string AElementFormat = null,
            IFormatProvider AFormatProvider = null)
        {
            if (AVector == null)
                throw new ArgumentNullException("AVector");
            if (AFormatMode == null)
                AFormatMode = "G";
            if (AElementFormat == null)
                AElementFormat = "G";
            if (AFormatProvider == null)
                AFormatProvider = CultureInfo.CurrentCulture;

            if (AFormatMode.Equals("G", StringComparison.Ordinal) ||
                AFormatMode.Equals("L", StringComparison.Ordinal) ||
                AFormatMode.Equals("linear", StringComparison.OrdinalIgnoreCase))
            {
                return String.Format(
                    "{0}{{{1}}}",
                    AVector.GetType().GetNeatName(),
                    Vector.FormatLinear(AVector, AElementFormat, AFormatProvider));
            }

            if (AFormatMode.Equals("T", StringComparison.Ordinal) ||
                AFormatMode.Equals("type", StringComparison.OrdinalIgnoreCase))
            {
                return String.Format(
                    "{0}{{{1} x <{2}>}}",
                    AVector.GetType().GetNeatName(),
                    AVector.Size,
                    AVector.ElementType.GetNeatName());
            }

            int iIndex;
            if (AFormatMode.Length == 1 && Char.IsLower(AFormatMode[0]))
            {
                iIndex = Convert.ToInt32(AFormatMode[0]) - Convert.ToInt32('a');
                return Vector.FormatElement(AVector, iIndex, AElementFormat, AFormatProvider);
            }

            if (int.TryParse(AFormatMode, out iIndex))
            {
                return Vector.FormatElement(AVector, iIndex, AElementFormat, AFormatProvider);
            }

            throw new FormatException("Unknown vector format string \"" + AFormatMode + "\"");
        }
        #endregion

        #region Mapping
        /// <summary>
        /// Maps the specified vector.
        /// </summary>
        /// <param name="ASource">The source vector.</param>
        /// <param name="ATarget">The target vector.</param>
        /// <param name="AMapper">The mapping function.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the arguments is null.</exception>
        /// <exception cref="System.ArgumentException">Target size does not match source size.</exception>
        public static void Map(
            IVector ASource,
            IMutableVector ATarget,
            Func<object, object> AMapper)
        {
            if (ASource == null)
                throw new ArgumentNullException("ASource");
            if (ATarget == null)
                throw new ArgumentNullException("ATarget");
            if (AMapper == null)
                throw new ArgumentNullException("AMapper");

            if (ASource.Size != ATarget.Size)
                throw new ArgumentException("Target size does not match source size.");

            for (int I = 0; I < ASource.Size; I++)
                ATarget.Set(I, AMapper(ASource.Get(I)));
        }
        /// <summary>
        /// Maps the specified strongly typed vector.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="ASource">The source vector.</param>
        /// <param name="ATarget">The target vector.</param>
        /// <param name="AMapper">The mapping function.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the arguments is null.</exception>
        public static void Map<TData>(
            IVector<TData> ASource,
            IMutableVector<TData> ATarget,
            Func<TData, TData> AMapper)
        {
            if (AMapper == null)
                throw new ArgumentNullException("AMapper");

            Vector.Map((IVector)ASource, ATarget, AIn => AMapper((TData)AIn));
        }
        #endregion

        #region Folding
        /// <summary>
        /// Folds the specified vector.
        /// </summary>
        /// <param name="AVector">The vector.</param>
        /// <param name="ANeutral">The neutral element to the folding operation.</param>
        /// <param name="AFolder">The folding operation.</param>
        /// <returns>The result of the folding operation.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the vector or the folder is null.</exception>
        public static object Fold(
            IVector AVector,
            object ANeutral,
            Func<object, object, object> AFolder)
        {
            if (AVector == null)
                throw new ArgumentNullException("AVector");
            if (AFolder == null)
                throw new ArgumentNullException("AFolder");

            object oResult = ANeutral;
            for (int I = 0; I < AVector.Size; I++)
                oResult = AFolder(oResult, AVector.Get(I));

            return oResult;
        }
        /// <summary>
        /// Folds the specified strongly typed vector.
        /// </summary>
        /// <typeparam name="TIn">The type of the vector elements.</typeparam>
        /// <typeparam name="TOut">The type of the folding result.</typeparam>
        /// <param name="AVector">The vector.</param>
        /// <param name="ANeutral">The neutral element to the folding operation.</param>
        /// <param name="AFolder">The folding operation.</param>
        /// <returns>The result of the folding operation.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the vector or the folder is null.</exception>
        public static TOut Fold<TIn, TOut>(
            IVector<TIn> AVector,
            TOut ANeutral,
            Func<TOut, TIn, TOut> AFolder)
        {
            if (AFolder == null)
                throw new ArgumentNullException("AFolder");

            return (TOut)Vector.Fold((IVector)AVector, ANeutral, (AState, ANext) => AFolder((TOut)AState, (TIn)ANext));
        }
        #endregion

        #region Combining
        /// <summary>
        /// Combines two vectors.
        /// </summary>
        /// <param name="ALeft">The left hand side vector.</param>
        /// <param name="ARight">The right hand side vector.</param>
        /// <param name="AOut">The output vector.</param>
        /// <param name="ACombinator">The combinator function.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the arguments is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the vector sizes do not match.</exception>
        public static void Combine(
            IVector ALeft,
            IVector ARight,
            IMutableVector AOut,
            Func<object, object, object> ACombinator)
        {
            if (ALeft == null)
                throw new ArgumentNullException("ALeft");
            if (ARight == null)
                throw new ArgumentNullException("ARight");
            if (AOut == null)
                throw new ArgumentNullException("AOut");
            if (ACombinator == null)
                throw new ArgumentNullException("ACombinator");

            if (ALeft.Size != ARight.Size)
                throw new ArgumentException("Input vector sizes do not match.");
            if (ALeft.Size != AOut.Size)
                throw new ArgumentException("Output vector sizes does not match.");

            for (int I = 0; I < ALeft.Size; I++)
                AOut.Set(I, ACombinator(ALeft.Get(I), ARight.Get(I)));
        }
        /// <summary>
        /// Combines two strongly typed vectors.
        /// </summary>
        /// <typeparam name="TLeft">The element type of the left vector.</typeparam>
        /// <typeparam name="TRight">The element type of the right vector.</typeparam>
        /// <typeparam name="TOut">The element type of the output vector.</typeparam>
        /// <param name="ALeft">The left hand side vector.</param>
        /// <param name="ARight">The right hand side vector.</param>
        /// <param name="AOut">The output vector.</param>
        /// <param name="ACombinator">The combinator function.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the arguments is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the vector sizes do not match.</exception>
        public static void Combine<TLeft, TRight, TOut>(
            IVector<TLeft> ALeft,
            IVector<TRight> ARight,
            IMutableVector<TOut> AOut,
            Func<TLeft, TRight, TOut> ACombinator)
        {
            if (ACombinator == null)
                throw new ArgumentNullException("ACombinator");

            Vector.Combine((IVector)ALeft, ARight, AOut, (AL, AR) => ACombinator((TLeft)AL, (TRight)AR));
        }
        #endregion
    }
}
