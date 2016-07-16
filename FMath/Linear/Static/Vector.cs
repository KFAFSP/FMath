using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;

using FMath.Linear.Generic;
using FMath.Linear.Generic.Proxy;

namespace FMath.Linear.Static
{
    public static class Vector
    {
        public const int C_HashSaltPrime = 409;
        [Pure]
        public static bool IsDefined(
            this IVector AVector,
            int AIndex)
        {
            if (AVector == null)
                throw new ArgumentNullException("AVector");

            return AIndex >= 0 && AIndex < AVector.Size;
        }

        #region Casting
        public static IVector<TOut> Cast<TOut>(this IVector AVector)
        {
            return new VectorCasterProxy<TOut>(AVector);
        }
        #endregion

        #region Equality
        [Pure]
        public static bool AreEqual(IVector ALeft, IVector ARight)
        {
            return Vector.AreEqual(ALeft, ARight, Object.Equals);
        }
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
        [Pure]
        public static int Hash(IVector AVector)
        {
            return Vector.Hash(AVector, EqualityComparer<object>.Default.GetHashCode);
        }
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
        [Pure]
        public static int Hash<TData>(
            IVector<TData> AVector,
            Func<TData, int> AHasher)
        {
            if (AHasher == null)
                throw new ArgumentNullException("AHasher");

            return Vector.Hash((IVector)AVector, AElement => AHasher((TData)AElement));
        }
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
        public static void Copy(
            IVector ASource,
            IMutableVector ATarget)
        {
            Vector.Copy(ASource, 0, ATarget, 0, ASource.Size);
        }
        #endregion

        #region String formatting
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
