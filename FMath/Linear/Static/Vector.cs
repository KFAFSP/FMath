using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

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

        #region String formatting
        /// <summary>
        /// Formats the specified vector.
        /// </summary>
        /// <param name="AVector">The vector.</param>
        /// <param name="AFormatMode">The format mode.</param>
        /// <param name="AElementFormat">The element format.</param>
        /// <param name="AFormatProvider">The format provider.</param>
        /// <returns>The resulting string.</returns>
        [Pure]
        public static string Format(
            IVector AVector,
            string AFormatMode = null,
            string AElementFormat = null,
            IFormatProvider AFormatProvider = null)
        {
            // TODO : Implement.
            return "";
        }
        #endregion
    }
}
