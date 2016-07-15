namespace FMath.Linear.Generic
{
    /// <summary>
    /// Generic interface for vector types with read-write access.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="FMath.Linear.IMutableVector" />
    /// <seealso cref="FMath.Linear.Generic.IVector{TData}" />
    public interface IMutableVector<TData> :
        IMutableVector,
        IVector<TData>
    {
        /// <summary>
        /// Sets the element at the specified index.
        /// </summary>
        /// <param name="AIndex">The zero-based index.</param>
        /// <param name="AValue">The value to set.</param>
        void Set(int AIndex, TData AValue);

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
        new TData this[int AIndex] { get; set; }
    }
}