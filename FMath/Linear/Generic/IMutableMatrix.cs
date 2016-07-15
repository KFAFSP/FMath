namespace FMath.Linear.Generic
{
    /// <summary>
    /// Generic interface for matrices with read-write access.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="FMath.Linear.IMutableMatrix" />
    /// <seealso cref="FMath.Linear.Generic.IMatrix{TData}" />
    public interface IMutableMatrix<TData> :
        IMutableMatrix,
        IMatrix<TData>
    {
        /// <summary>
        /// Sets the element at the specified index.
        /// </summary>
        /// <param name="AIndices">The zero-based cell indices.</param>
        /// <param name="AValue">The value to set.</param>
        void Set(MatrixIndices AIndices, TData AValue);

        /// <summary>
        /// Gets or sets an element of the matrix.
        /// </summary>
        /// <value>
        /// The element at the index.
        /// </value>
        /// <param name="ARow">The zero-based row index.</param>
        /// <param name="ACol">The zero-based column index.</param>
        /// <returns>
        /// The element at the index.
        /// </returns>
        new TData this[int ARow, int ACol] { get; set; }
    }
}