using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;

using FMath.Linear.Generic;
using FMath.Linear.Generic.Proxy;

namespace FMath.Linear.Static
{
    public static class Matrix
    {
        public const int C_HashSaltPrime = 499;

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

        #region Casting
        public static IMatrix<TOut> Cast<TOut>(this IMatrix AMatrix)
        {
            return new MatrixCasterProxy<TOut>(AMatrix);
        }
        #endregion

        #region Linear indexing
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
        [Pure]
        public static bool AreEqual(IMatrix ALeft, IMatrix ARight)
        {
            return Matrix.AreEqual(ALeft, ARight, Object.Equals);
        }
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

            if (object.ReferenceEquals(ALeft, ARight))
                return true;

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
        [Pure]
        public static int Hash(IMatrix AMatrix)
        {
            return Matrix.Hash(AMatrix, EqualityComparer<object>.Default.GetHashCode);
        }
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
        [Pure]
        public static int Hash<TData>(
            IMatrix<TData> AMatrix,
            Func<TData, int> AHasher)
        {
            if (AHasher == null)
                throw new ArgumentNullException("AHasher");

            return Matrix.Hash((IMatrix)AMatrix, AElement => AHasher((TData)AElement));
        }
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
        public static void Copy(
            IMatrix ASource,
            IMutableMatrix ATarget)
        {
            Matrix.Copy(ASource, MatrixIndices.Zero, ATarget, MatrixIndices.Zero, ASource.Size);
        }
        #endregion

        #region String formatting
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
        public static void Map<TData>(
            IMatrix<TData> ASource,
            IMutableMatrix<TData> ATarget,
            Func<TData, TData> AMapper)
        {
            if (AMapper == null)
                throw new ArgumentNullException("AMapper");

            Matrix.Map((IMatrix)ASource, ATarget, AIn => AMapper((TData)AIn));
        }

        public static IMatrix<TOut> Map<TIn, TOut>(
            this IMatrix<TIn> AIn,
            Func<TIn, TOut> AMapper)
        {
            if (AIn == null)
                throw new ArgumentNullException("AIn");
            if (AMapper == null)
                throw new ArgumentNullException("AMapper");

            return new MatrixMappingProxy<TIn, TOut>(AIn, AMapper, null);
        }
        public static IMutableMatrix<TOut> Map<TIn, TOut>(
            this IMutableMatrix<TIn> AIn,
            Func<TIn, TOut> AForward,
            Func<TOut, TIn> AReverse)
        {
            if (AIn == null)
                throw new ArgumentNullException("AIn");
            if (AForward == null)
                throw new ArgumentNullException("AForward");
            if (AReverse == null)
                throw new ArgumentNullException("AReverse");

            return new MatrixMappingProxy<TIn, TOut>(AIn, AForward, AReverse);
        }
        #endregion

        #region Combining
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

            if (object.ReferenceEquals(ALeft, AOutput) || object.ReferenceEquals(ARight, AOutput))
                throw new ArgumentException("Matrix multiplication cannot be performed in-place.");

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
