using System.Collections.Generic;

namespace FMath.Linear.Generic
{
    /// <summary>
    /// Generic interface for vector types with at least read access.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="FMath.Linear.IVector" />
    /// <seealso cref="System.Collections.Generic.IEnumerable{TData}" />
    public interface IVector<out TData> :
        IVector,
        IEnumerable<TData>
    {
        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="AIndex">The zero-based index.</param>
        /// <returns>The element at the index.</returns>
        new TData Get(int AIndex);

        /// <summary>
        /// Gets an element of the vector.
        /// </summary>
        /// <value>
        /// The element at the index.
        /// </value>
        /// <param name="AIndex">The zero-based element index.</param>
        /// <returns>The element at the index.</returns>
        new TData this[int AIndex] { get; }
    }
}