using System;
using System.Linq;

using FMath.Linear.Generic.Immutable;

namespace FMath.Linear.Static
{
    /// <summary>
    /// Static factory class for <see cref="ArrayVector{TData}"/> structs.
    /// </summary>
    public static class ArrayVector
    {
        /// <summary>
        /// Gets an empty <see cref="ArrayVector{TData}"/> struct.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <returns>The empty vector.</returns>
        public static ArrayVector<TData> Empty<TData>()
        {
            return new ArrayVector<TData>();
        }

        /// <summary>
        /// Gets a <see cref="ArrayVector{TData}"/> that is filled with the default (zero) element of the stored type.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="ASize">The vector size.</param>
        /// <returns>The zero vector.</returns>
        public static ArrayVector<TData> Zero<TData>(int ASize)
        {
            return ArrayVector.Filled<TData>(ASize);
        }

        /// <summary>
        /// Gets a <see cref="ArrayVector{TData}"/> that is filled with the specified default element.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="ASize">The vector size.</param>
        /// <param name="AFill">The fill element.</param>
        /// <returns>The filled vector.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when ASize is negative.</exception>
        public static ArrayVector<TData> Filled<TData>(int ASize, TData AFill = default(TData))
        {
            if (ASize < 0)
                throw new ArgumentOutOfRangeException("ASize");

            if (!AFill.Equals(default(TData)))
                return Enumerable.Repeat(AFill, ASize).ToArrayVector();

            return new ArrayVector<TData>(new TData[ASize], false);
        }

        /// <summary>
        /// Packs the specified data into an <see cref="ArrayVector{TData}"/>.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="AData">The data.</param>
        /// <returns>The packed vector.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when AData is null.</exception>
        public static ArrayVector<TData> Pack<TData>(params TData[] AData)
        {
            if (AData == null)
                throw new ArgumentNullException("AData");

            return new ArrayVector<TData>(AData);
        }
    }
}
