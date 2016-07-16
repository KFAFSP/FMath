using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;

using FMath.Linear.Generic;

namespace FMath.Linear.Static
{
    /// <summary>
    /// Static class for handling <see cref="IMatrix"/> instances.
    /// </summary>
    public static class Matrix
    {
        /// <summary>
        /// The salting prime used in the matrix hashing function.
        /// </summary>
        public const int C_HashSaltPrime = 499;

        /// <summary>
        /// Checks whether the specified indices are defined in the matrix.
        /// </summary>
        /// <param name="AMatrix">The matrix.</param>
        /// <param name="AIndices">The indices.</param>
        /// <returns><c>true</c> if the indices are defined, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when AMatrix is null.</exception>
        [Pure]
        public static bool AreDefined(
            this IMatrix AMatrix,
            MatrixIndices AIndices)
        {
            if (AMatrix == null)
                throw new ArgumentNullException("AMatrix");

            return AIndices.M >= 0 && AIndices.N >= 0
                   && AIndices.M < AMatrix.Size.M && AIndices.N < AMatrix.Size.N;
        }

        #region Linear indexing
        /// <summary>
        /// Converts an offset to a pair of indices.
        /// </summary>
        /// <param name="AMatrix">The matrix.</param>
        /// <param name="AOffset">The offset.</param>
        /// <returns>The indices.</returns>
        /// <exception cref="ArgumentNullException">Thrown when AMatrix is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when AOffset is out of range.</exception>
        public static MatrixIndices OffsetToIndices(
             this IMatrix AMatrix,
             int AOffset)
        {
            if (AMatrix == null)
                throw new ArgumentNullException("AMatrix");

            MatrixIndices miIndices = new MatrixIndices(
                AOffset / AMatrix.Size.N,
                AOffset % AMatrix.Size.N);

            if (!AMatrix.AreDefined(miIndices))
                throw new ArgumentOutOfRangeException("AOffset");

            return miIndices;
        }
        /// <summary>
        /// Converts a pair of indices to an offset.
        /// </summary>
        /// <param name="AMatrix">The matrix.</param>
        /// <param name="AIndices">The indices.</param>
        /// <returns>The offset.</returns>
        /// <exception cref="ArgumentNullException">Thrown when AMatrix is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown AIndices are out of range.</exception>
        public static int IndicesToOffset(
            this IMatrix AMatrix,
            MatrixIndices AIndices)
        {
            if (AMatrix == null)
                throw new ArgumentNullException("AMatrix");
           if (!AMatrix.AreDefined(AIndices))
                throw new ArgumentOutOfRangeException("AIndices");

            return AIndices.M * AMatrix.Size.N + AIndices.N;
        }
        #endregion

        #region Equality
        /// <summary>
        /// Checks whether two matrices are equal by comparing their elements.
        /// </summary>
        /// <param name="ALeft">The left matrix.</param>
        /// <param name="ARight">The right matrix.</param>
        /// <returns><c>true</c> if they are equal, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static bool AreEqual(IMatrix ALeft, IMatrix ARight)
        {
            return Matrix.AreEqual(ALeft, ARight, Object.Equals);
        }
        /// <summary>
        /// Checks whether two matrices are equal by comparing their elements.
        /// </summary>
        /// <param name="ALeft">The left matrix.</param>
        /// <param name="ARight">The right matrix.</param>
        /// <param name="AEquator">The element equator.</param>
        /// <returns><c>true</c> if they are equal, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static bool AreEqual(
            IMatrix ALeft,
            IMatrix ARight,
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

            for (int M = 0; M < ALeft.Size.M; M++)
                for (int N = 0; N < ARight.Size.N; N++)
                {
                    MatrixIndices miIndices = new MatrixIndices(M, N);
                    if (!AEquator(ALeft.Get(miIndices), ARight.Get(miIndices)))
                        return false;
                }

            return true;
        }
        /// <summary>
        /// Checks whether two strongly typed matrices are equal by comparing their elements.
        /// </summary>
        /// <typeparam name="TLeft">The element type of the left matrix.</typeparam>
        /// <typeparam name="TRight">The element type of the right matrix.</typeparam>
        /// <param name="ALeft">The left matrix.</param>
        /// <param name="ARight">The right matrix.</param>
        /// <param name="AEquator">The element equator.</param>
        /// <returns>
        ///   <c>true</c> if they are equal, <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static bool AreEqual<TLeft, TRight>(
            IMatrix<TLeft> ALeft,
            IMatrix<TRight> ARight,
            Func<TLeft, TRight, bool> AEquator)
        {
            if (AEquator == null)
                throw new ArgumentNullException("AEquator");

            return Matrix.AreEqual((IMatrix)ALeft, ARight, (AL, AR) => AEquator((TLeft)AL, (TRight)AR));
        }
        /// <summary>
        /// Checks whether two strongly typed matrices are equal by comparing their elements.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="ALeft">The left matrix.</param>
        /// <param name="ARight">The right matrix.</param>
        /// <param name="AComparer">The element comparer, or null to use the default comparer.</param>
        /// <returns>
        ///   <c>true</c> if they are equal, <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static bool AreEqual<TData>(
            IMatrix<TData> ALeft,
            IMatrix<TData> ARight,
            IEqualityComparer<TData> AComparer = null)
        {
            if (AComparer == null)
                AComparer = EqualityComparer<TData>.Default;

            return Matrix.AreEqual(ALeft, ARight, AComparer.Equals);
        }
        #endregion

        #region Hashing
        /// <summary>
        /// Hashes the specified matrix.
        /// </summary>
        /// <param name="AMatrix">A matrix.</param>
        /// <returns>An element-wise computed hash that also contains the size of the matrix.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static int Hash(IMatrix AMatrix)
        {
            return Matrix.Hash(AMatrix, EqualityComparer<object>.Default.GetHashCode);
        }
        /// <summary>
        /// Hashes the specified matrix.
        /// </summary>
        /// <param name="AMatrix">The matrix.</param>
        /// <param name="AHasher">The element hashing delegate.</param>
        /// <returns>An element-wise computed hash that also contains the size of the matrix.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static int Hash(
            IMatrix AMatrix,
            Func<object, int> AHasher)
        {
            if (AMatrix == null)
                throw new ArgumentNullException("AMatrix");
            if (AHasher == null)
                throw new ArgumentNullException("AHasher");

            int iHash = unchecked (AMatrix.Size.M*Vector.C_HashSaltPrime + AMatrix.Size.N);
            for (int M = 0; M < AMatrix.Size.M; M++)
                for (int N = 0; N < AMatrix.Size.N; N++)
                    iHash = unchecked(iHash * Vector.C_HashSaltPrime + AHasher(AMatrix.Get(new MatrixIndices(M, N))));

            return iHash;
        }
        /// <summary>
        /// Hashes the specified strongly typed matrix.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="AMatrix">The matrix.</param>
        /// <param name="AHasher">The element hashing delegate.</param>
        /// <returns>An element-wise computed hash that also contains the size of the matrix.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static int Hash<TData>(
            IMatrix<TData> AMatrix,
            Func<TData, int> AHasher)
        {
            if (AHasher == null)
                throw new ArgumentNullException("AHasher");

            return Matrix.Hash((IMatrix)AMatrix, AElement => AHasher((TData)AElement));
        }
        /// <summary>
        /// Hashes the specified strongly typed matrix.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="AMatrix">The matrix.</param>
        /// <param name="AComparer">The element comparer, or null to use the default comparer.</param>
        /// <returns>An element-wise computed hash that also contains the size of the matrix.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the arguments is null.</exception>
        [Pure]
        public static int Hash<TData>(
            IMatrix<TData> AMatrix,
            IEqualityComparer<TData> AComparer = null)
        {
            if (AComparer == null)
                AComparer = EqualityComparer<TData>.Default;

            return Matrix.Hash(AMatrix, AComparer.GetHashCode);
        }
        #endregion

        #region Copying
        /// <summary>
        /// Copies data from one matrix into another.
        /// </summary>
        /// <param name="ASource">The source matrix.</param>
        /// <param name="ASourceOffset">The source offset indices.</param>
        /// <param name="ATarget">The target matrix.</param>
        /// <param name="ATargetOffset">The target offset indices.</param>
        /// <param name="ACount">The number of elements to copy.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when one of the matrices is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the operation would exceed the matrix bounds.</exception>
        public static void Copy(
            IMatrix ASource,
            MatrixIndices ASourceOffset,
            IMutableMatrix ATarget,
            MatrixIndices ATargetOffset,
            MatrixIndices ACount)
        {
            if (ASource == null)
                throw new ArgumentNullException("ASource");
            if (!ASource.AreDefined(ASourceOffset))
                throw new ArgumentOutOfRangeException("ASourceOffset");

            if (ATarget == null)
                throw new ArgumentNullException("ATarget");
            if (!ATarget.AreDefined(ATargetOffset))
                throw new ArgumentOutOfRangeException("ATargetOffset");

            if (ACount.M == 0 || ACount.N == 0)
                return;
            if (ACount.M < 0 || ACount.N < 0)
                throw new ArgumentOutOfRangeException("ACount");
            if (!ASource.AreDefined(ASourceOffset + ACount - MatrixIndices.One))
                throw new ArgumentOutOfRangeException("ACount");
            if (!ATarget.AreDefined(ATargetOffset + ACount - MatrixIndices.One))
                throw new ArgumentOutOfRangeException("ACount");

            for (int M = 0; M < ACount.M; M++)
                for (int N = 0; N < ACount.N; N++)
                {
                    MatrixIndices miLocal = new MatrixIndices(M, N);
                    ATarget.Set(ATargetOffset + miLocal, ASource.Get(ASourceOffset + miLocal));
                }
        }
        /// <summary>
        /// Copies all data from one matrix into another.
        /// </summary>
        /// <param name="ASource">The source matrix.</param>
        /// <param name="ATarget">The target matrix.</param>
        public static void Copy(
            IMatrix ASource,
            IMutableMatrix ATarget)
        {
            Matrix.Copy(ASource, MatrixIndices.Zero, ATarget, MatrixIndices.Zero, ASource.Size);
        }
        #endregion

        #region String formatting
        /// <summary>
        /// Formats a cell of the matrix.
        /// </summary>
        /// <param name="AMatrix">The matrix.</param>
        /// <param name="AIndices">The indices of the cell.</param>
        /// <param name="ACellFormat">The cell format.</param>
        /// <param name="AFormatProvider">The format provider.</param>
        /// <returns> The formatted cell. </returns>
        /// <exception cref="ArgumentNullException">Thrown when AMatrix is null.</exception>
        [Pure]
        public static string FormatCell(
            IMatrix AMatrix,
            MatrixIndices AIndices,
            string ACellFormat = null,
            IFormatProvider AFormatProvider = null)
        {
            if (AMatrix == null)
                throw new ArgumentNullException("AMatrix");

            return AMatrix.Get(AIndices).SafeFormat(ACellFormat, AFormatProvider);
        }
        /// <summary>
        /// Linearizes the specified matrix.
        /// </summary>
        /// <param name="AMatrix">aThe matrix.</param>
        /// <param name="ACellFormat">The cell format.</param>
        /// <param name="AFormatProvider">The format provider.</param>
        /// <returns>
        /// A linear representation of the matrix.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when AMatrix is null.</exception>
        [Pure]
        public static string FormatLinear(
            IMatrix AMatrix,
            string ACellFormat = null,
            IFormatProvider AFormatProvider = null)
        {
            if (AMatrix == null)
                throw new ArgumentNullException("AMatrix");

            StringBuilder sbResult = new StringBuilder();

            sbResult.Append("[");

            for (int M = 0; M < AMatrix.Size.M; M++)
            {
                if (M != 0)
                    sbResult.Append(", ");

                sbResult.Append("[");
                for (int N = 0; N < AMatrix.Size.N; N++)
                {
                    if (N != 0)
                        sbResult.Append(", ");

                    sbResult.Append(Matrix.FormatCell(AMatrix, new MatrixIndices(M, N), ACellFormat, AFormatProvider));
                }
                sbResult.Append("]");
            }

            sbResult.Append("]");

            return sbResult.ToString();
        }
        /// <summary>
        /// Formats the specified matrix.
        /// </summary>
        /// <param name="AMatrix">The matrix.</param>
        /// <param name="AFormatMode">The format mode.</param>
        /// <param name="ACellFormat">The cell format.</param>
        /// <param name="AFormatProvider">The format provider.</param>
        /// <returns>The resulting string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when AMatrix is null.</exception>
        /// <remarks>
        /// The following format options are supported:
        /// <list type="table">
        ///     <listheader>
        ///         <term>AFormatMode</term>
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item>
        ///         <term>null, "G"(CD), "L"(CD), "linear"(ci)</term>
        ///         <description>Linearizes the matrix.</description>
        ///     </item>
        ///     <item>
        ///         <term>"T"(CD), "type"(ci)</term>
        ///         <description>Outputs the element type and the size of the matrix.</description>
        ///     </item>
        ///     <item>
        ///         <term>'a'-'z'(CD)</term>
        ///         <description>Outputs the specified named element (converted to zero-based index starting with a=0).</description>
        ///     </item> 
        ///     <item>
        ///         <term>&lt;number&gt;&lt;number&gt;</term>
        ///         <description>
        ///             Outputs the specified indexed element. Note that this string needs to be a fixed with address,
        ///             meaning both numbers have to be padded with leading zeroes. The length of an address part must be
        ///             ceil(log10(max(AMatrix.Size.M, AMatrix.Size.N))) so that it is well defined. These addresses make an expcetion,
        ///             as they are one-based instead of zero-based, so that "(0...)1(0...)1" is the first cell of the matrix.
        ///         </description>
        ///     </item>
        /// </list>
        /// </remarks>
        [Pure]
        public static string Format(
            IMatrix AMatrix,
            string AFormatMode = null,
            string ACellFormat = null,
            IFormatProvider AFormatProvider = null)
        {
            if (AMatrix == null)
                throw new ArgumentNullException("AMatrix");
            if (AFormatMode == null)
                AFormatMode = "G";
            if (ACellFormat == null)
                ACellFormat = "G";
            if (AFormatProvider == null)
                AFormatProvider = CultureInfo.CurrentCulture;

            if (AFormatMode.Equals("G", StringComparison.Ordinal) ||
                AFormatMode.Equals("L", StringComparison.Ordinal) ||
                AFormatMode.Equals("linear", StringComparison.OrdinalIgnoreCase))
            {
                return String.Format(
                    "{0}{{{1}}}",
                    AMatrix.GetType().GetNeatName(),
                    Matrix.FormatLinear(AMatrix, ACellFormat, AFormatProvider));
            }

            if (AFormatMode.Equals("T", StringComparison.Ordinal) ||
                AFormatMode.Equals("type", StringComparison.OrdinalIgnoreCase))
            {
                return String.Format(
                    "{0}{{{1}x{2} x <{3}>}}",
                    AMatrix.GetType().GetNeatName(),
                    AMatrix.Size.M, AMatrix.Size.N,
                    AMatrix.ElementType.GetNeatName());
            }

            if (AFormatMode.Length == 1 && Char.IsLower(AFormatMode[0]))
            {
                int iOffset = Convert.ToInt32(AFormatMode[0]) - Convert.ToInt32('a');
                return Matrix.FormatCell(AMatrix, AMatrix.OffsetToIndices(iOffset), ACellFormat, AFormatProvider);
            }

            int iAddrWidth = (int)Math.Ceiling(Math.Log10(Math.Max(AMatrix.Size.M, AMatrix.Size.N)));
            if (AFormatMode.Length == iAddrWidth * 2)
            {
                string sRow = AFormatMode.Substring(0, iAddrWidth), sCol = AFormatMode.Substring(iAddrWidth, iAddrWidth);
                int iRow, iCol;
                if (int.TryParse(sRow, out iRow) && int.TryParse(sCol, out iCol))
                    return Matrix.FormatCell(AMatrix, new MatrixIndices(iRow - 1, iCol - 1), ACellFormat, AFormatProvider);
            }

            throw new FormatException("Unknown matrix format string \"" + AFormatMode + "\"");
        }
        #endregion

        #region Mapping
        /// <summary>
        /// Maps the specified matrix.
        /// </summary>
        /// <param name="ASource">The source matrix.</param>
        /// <param name="ATarget">The target matrix.</param>
        /// <param name="AMapper">The mapping function.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the arguments is null.</exception>
        /// <exception cref="System.ArgumentException">Target size does not match source size.</exception>
        public static void Map(
            IMatrix ASource,
            IMutableMatrix ATarget,
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

            for (int N = 0; N < ASource.Size.N; N++)
                for (int M = 0; M < ASource.Size.M; M++)
                {
                    MatrixIndices miIndices = new MatrixIndices(M, N);
                    ATarget.Set(miIndices, AMapper(ASource.Get(miIndices)));
                }
        }
        /// <summary>
        /// Maps the specified strongly typed matrix.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="ASource">The source matrix.</param>
        /// <param name="ATarget">The target matrix.</param>
        /// <param name="AMapper">The mapping function.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the arguments is null.</exception>
        public static void Map<TData>(
            IMatrix<TData> ASource,
            IMutableMatrix<TData> ATarget,
            Func<TData, TData> AMapper)
        {
            if (AMapper == null)
                throw new ArgumentNullException("AMapper");

            Matrix.Map((IMatrix)ASource, ATarget, AIn => AMapper((TData)AIn));
        }
        #endregion

        #region Combining
        /// <summary>
        /// Combines two matrices.
        /// </summary>
        /// <param name="ALeft">The left hand side matrix.</param>
        /// <param name="ARight">The right hand side matrix.</param>
        /// <param name="AOut">The output matrix.</param>
        /// <param name="ACombinator">The combinator function.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the arguments is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the matrix sizes do not match.</exception>
        public static void Combine(
            IMatrix ALeft,
            IMatrix ARight,
            IMutableMatrix AOut,
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

            for (int N = 0; N < ALeft.Size.N; N++)
                for (int M = 0; M < ALeft.Size.M; M++)
                {
                    MatrixIndices miIndices = new MatrixIndices(M, N);
                    AOut.Set(miIndices, ACombinator(ALeft.Get(miIndices), ARight.Get(miIndices)));
                }
        }
        /// <summary>
        /// Combines two strongly typed matrices.
        /// </summary>
        /// <typeparam name="TLeft">The element type of the left matrix.</typeparam>
        /// <typeparam name="TRight">The element type of the right matrix.</typeparam>
        /// <typeparam name="TOut">The element type of the output matrix.</typeparam>
        /// <param name="ALeft">The left hand side matrix.</param>
        /// <param name="ARight">The right hand side matrix.</param>
        /// <param name="AOut">The output matrix.</param>
        /// <param name="ACombinator">The combinator function.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the arguments is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the matrix sizes do not match.</exception>
        public static void Combine<TLeft, TRight, TOut>(
            IMatrix<TLeft> ALeft,
            IMatrix<TRight> ARight,
            IMutableMatrix<TOut> AOut,
            Func<TLeft, TRight, TOut> ACombinator)
        {
            if (ACombinator == null)
                throw new ArgumentNullException("ACombinator");

            Matrix.Combine((IMatrix)ALeft, ARight, AOut, (AL, AR) => ACombinator((TLeft)AL, (TRight)AR));
        }
        #endregion

        #region Multiplying
        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        /// <param name="ALeft">The left matrix.</param>
        /// <param name="ARight">The right matrix.</param>
        /// <param name="AOutput">The output matrix.</param>
        /// <param name="AMultiplication">The multiplication operation.</param>
        /// <param name="AAddition">The addition operation.</param>
        /// <param name="AZero">The neutral element of the addition operation.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the arguments is null, except the neutral element.</exception>
        /// <exception cref="System.ArgumentException">The multiplication cannot be performed.</exception>
        public static void Multiply(
            IMatrix ALeft,
            IMatrix ARight,
            IMutableMatrix AOutput,
            Func<object, object, object> AMultiplication,
            Func<object, object, object> AAddition,
            object AZero)
        {
            if (ALeft == null)
                throw new ArgumentNullException("ALeft");
            if (ARight == null)
                throw new ArgumentNullException("ARight");
            if (AOutput == null)
                throw new ArgumentNullException("AOutput");
            if (AMultiplication == null)
                throw new ArgumentNullException("AMultiplication");
            if (AAddition == null)
                throw new ArgumentNullException("AAddition");

            if (ALeft.Size.N != ARight.Size.M)
                throw new ArgumentException("Matrices are not multiplyable.");
            if (AOutput.Size.M != ALeft.Size.M || AOutput.Size.N != ARight.Size.N)
                throw new ArgumentException("Output matrix has the wrong size.");

            for (int N = 0; N < AOutput.Size.N; N++)
                for (int M = 0; M < AOutput.Size.M; M++)
                {
                    MatrixIndices miOut = new MatrixIndices(N, M);
                    object oCell = AZero;

                    for (int I = 0; I < ALeft.Size.N; I++)
                    {
                        MatrixIndices miLeft = new MatrixIndices(miOut.M, I);
                        MatrixIndices miRight = new MatrixIndices(I, miOut.N);
                        oCell = AAddition(oCell, AMultiplication(ALeft.Get(miLeft), ARight.Get(miRight)));
                    }

                    AOutput.Set(miOut, oCell);
                }
        }
        /// <summary>
        /// Multiplies two strongly typed matrices.
        /// </summary>
        /// <typeparam name="TIn">The element type of the input matrices.</typeparam>
        /// <typeparam name="TOut">The element type of the output matrix.</typeparam>
        /// <param name="ALeft">The left matrix.</param>
        /// <param name="ARight">The right matrix.</param>
        /// <param name="AOutput">The output matrix.</param>
        /// <param name="AMultiplication">The multiplication operation.</param>
        /// <param name="AAddition">The addition operation.</param>
        /// <param name="AZero">The neutral element of the addition operation.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the arguments is null, except the neutral element.</exception>
        /// <exception cref="System.ArgumentException">The multiplication cannot be performed.</exception>
        public static void Multiply<TIn, TOut>(
            IMatrix<TIn> ALeft,
            IMatrix<TIn> ARight,
            IMutableMatrix<TOut> AOutput,
            Func<TIn, TIn, TOut> AMultiplication,
            Func<TOut, TOut, TOut> AAddition,
            TOut AZero)
        {
            if (AMultiplication == null)
                throw new ArgumentNullException("AMultiplication");
            if (AAddition == null)
                throw new ArgumentNullException("AAddition");

            Matrix.Multiply(
                (IMatrix)ALeft,
                ARight,
                AOutput,
                (AL, AR) => AMultiplication((TIn)AL, (TIn)AR),
                (AL, AR) => AAddition((TOut)AL, (TOut)AR),
                AZero);
        }
        #endregion
    }
}
