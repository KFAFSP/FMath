using System;
using System.Collections;

namespace FMath.Linear
{
    /// <summary>
    /// Interface for vector types with at least read access.
    /// </summary>
    /// <seealso cref="FMath.IStructure" />
    /// <seealso cref="System.ICloneable" />
    /// <seealso cref="System.Collections.IEnumerable" />
    public interface IVector :
        IStructure,
        ICloneable,
        IEnumerable
    {
        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="AIndex">The zero-based index.</param>
        /// <returns>The element at the index.</returns>
        object Get(int AIndex);

        /// <summary>
        /// Gets the size of the vector.
        /// </summary>
        /// <value>
        /// The size of the vector.
        /// </value>
        int Size { get; }

        /// <summary>
        /// Gets an element of the vector.
        /// </summary>
        /// <value>
        /// The element at the index.
        /// </value>
        /// <param name="AIndex">The zero-based element index.</param>
        /// <returns>The element at the index.</returns>
        object this[int AIndex] { get; }
    }
}
