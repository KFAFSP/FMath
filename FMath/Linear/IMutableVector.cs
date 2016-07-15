namespace FMath.Linear
{
    /// <summary>
    /// Interface for vector types with read-write access.
    /// </summary>
    /// <seealso cref="FMath.Linear.IVector" />
    /// <seealso cref="FMath.IAssignable{FMath.Linear.IVector}" />
    public interface IMutableVector :
        IVector,
        IAssignable<IVector>
    {
        /// <summary>
        /// Sets the element at the specified index.
        /// </summary>
        /// <param name="AIndex">The zero-based index.</param>
        /// <param name="AValue">The value to set.</param>
        void Set(int AIndex, object AValue);

        /// <summary>
        /// Gets or sets an element of the vector.
        /// </summary>
        /// <value>
        /// The element at the index.
        /// </value>
        /// <param name="AIndex">The zero-based element index.</param>
        /// <returns>
        /// The element at the index.
        /// </returns>
        new object this[int AIndex] { get; set; }
    }
}