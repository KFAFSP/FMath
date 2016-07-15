using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

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

        #region String formatting
        /// <summary>
        /// Formats the specified matrix.
        /// </summary>
        /// <param name="AMatrix">The matrix.</param>
        /// <param name="AFormatMode">The format mode.</param>
        /// <param name="ACellFormat">The cell format.</param>
        /// <param name="AFormatProvider">The format provider.</param>
        /// <returns>The resulting string.</returns>
        [Pure]
        public static string Format(
            IMatrix AMatrix,
            string AFormatMode = null,
            string ACellFormat = null,
            IFormatProvider AFormatProvider = null)
        {
            // TODO : Implement.
            return "";
        }
        #endregion
    }
}
