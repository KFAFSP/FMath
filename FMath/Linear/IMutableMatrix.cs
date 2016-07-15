namespace FMath.Linear
{
    /// <summary>
    /// Interface for matrix types with read-write access.
    /// </summary>
    /// <seealso cref="FMath.Linear.IMatrix" />
    /// <seealso cref="FMath.IAssignable{FMath.Linear.IMatrix}" />
    public interface IMutableMatrix :
        IMatrix,
        IAssignable<IMatrix>
    {
        /// <summary>
        /// Sets the element at the specified index.
        /// </summary>
        /// <param name="AIndices">The zero-based cell indices.</param>
        /// <param name="AValue">The value to set.</param>
        void Set(MatrixIndices AIndices, object AValue);

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
        new object this[int ARow, int ACol] { get; set; }
    }
}