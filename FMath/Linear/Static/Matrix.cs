using System;
using System.Diagnostics.Contracts;

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

        #region Basic operations        
        /// <summary>
        /// Checks whether two matrices are equal by comparing their elements.
        /// </summary>
        /// <param name="ALeft">The left matrix.</param>
        /// <param name="ARight">The right matrix.</param>
        /// <returns><c>true</c> if they are equal, <c>false</c> otherwise.</returns>
        [Pure]
        public static bool AreEqual(IMatrix ALeft, IMatrix ARight)
        {
            // TODO : Implement
            return false;
        }
        /// <summary>
        /// Hashes the specified matrix.
        /// </summary>
        /// <param name="AMatrix">A matrix.</param>
        /// <returns>An element-wise computed hash that also contains the size of the matrix.</returns>
        [Pure]
        public static int Hash(IMatrix AMatrix)
        {
            // TODO : Implement
            return 0;
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
