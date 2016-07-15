namespace FMath.Linear.Generic
{
    /// <summary>
    /// Generic interface for matrices with at least read access.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="FMath.Linear.IMatrix" />
    public interface IMatrix<out TData> :
        IMatrix
    {
        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="AIndices">The zero-based cell indices.</param>
        /// <returns>The element at the index.</returns>
        new TData Get(MatrixIndices AIndices);

        /// <summary>
        /// Gets an element of the matrix.
        /// </summary>
        /// <value>
        /// The element at the index.
        /// </value>
        /// <param name="ARow">The zero-based row index.</param>
        /// <param name="ACol">The zero-based column index.</param>
        /// <returns>The element at the index.</returns>
        new TData this[int ARow, int ACol] { get; }
    }
}
