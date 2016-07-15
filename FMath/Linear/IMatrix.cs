using System;

namespace FMath.Linear
{
    /// <summary>
    /// Interface for matrix types with at least read access.
    /// </summary>
    /// <seealso cref="FMath.IStructure" />
    /// <seealso cref="System.ICloneable" />
    public interface IMatrix :
        IStructure,
        ICloneable
    {
        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="AIndices">The zero-based cell indices.</param>
        /// <returns>The element at the index.</returns>
        object Get(MatrixIndices AIndices);

        /// <summary>
        /// Gets the size of the matrix.
        /// </summary>
        /// <value>
        /// The size of the matrix.
        /// </value>
        MatrixIndices Size { get; }

        /// <summary>
        /// Gets an element of the matrix.
        /// </summary>
        /// <value>
        /// The element at the index.
        /// </value>
        /// <param name="ARow">The zero-based row index.</param>
        /// <param name="ACol">The zero-based column index.</param>
        /// <returns>The element at the index.</returns>
        object this[int ARow, int ACol] { get; }
    }
}