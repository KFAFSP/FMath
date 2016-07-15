﻿using System;

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
        public static bool IsDefined(
            this IVector AVector,
            int AIndex)
        {
            if (AVector == null)
                throw new ArgumentNullException("AVector");

            return AIndex >= 0 && AIndex < AVector.Size;
        }

        #region Basic operations
        /// <summary>
        /// Checks whether two vectors are equal by comparing their elements.
        /// </summary>
        /// <param name="ALeft">The left vector.</param>
        /// <param name="ARight">The right vector.</param>
        /// <returns><c>true</c> if they are equal, <c>false</c> otherwise.</returns>
        public static bool AreEqual(IVector ALeft, IVector ARight)
        {
            // TODO : Implement
            return false;
        }
        /// <summary>
        /// Hashes the specified vector.
        /// </summary>
        /// <param name="AVector">A vector.</param>
        /// <returns>An element-wise computed hash that also contains the size of the vector.</returns>
        public static int Hash(IVector AVector)
        {
            // TODO : Implement
            return 0;
        }
        #endregion
    }
}
