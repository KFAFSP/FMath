using FMath.Linear.Generic.Base;

namespace FMath.Linear.Generic.Mutable
{
    /// <summary>
    /// Mutable vector that is implemented using an array.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="MutableVectorBase{TData}" />
    public class DenseVector<TData> :
        MutableVectorBase<TData>
    {
        protected readonly TData[] FElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseVector{TData}"/> class.
        /// </summary>
        /// <param name="ASize">The size of the vector.</param>
        public DenseVector(int ASize)
            : base(ASize)
        {
            this.FElements = new TData[3];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DenseVector{TData}"/> class.
        /// </summary>
        /// <param name="AElements">The array of elements..</param>
        /// <param name="ACopy">If set to <c>true</c> a copy of the element array will be created, otherwise the reference will be assigned.</param>
        public DenseVector(TData[] AElements, bool ACopy = true)
            : base(AElements != null ? AElements.Length : 0)
        {
            if (AElements == null)
                this.FElements = new TData[0];
            else
                this.FElements = ACopy
                    ? (TData[])AElements.Clone()
                    : AElements;
        }

        protected internal override TData DirectGet(int AIndex)
        {
            return this.FElements[AIndex];
        }
        protected internal override void DirectSet(int AIndex, TData AData)
        {
            this.FElements[AIndex] = AData;
        }

        /// <inheritDoc />
        public override object Clone()
        {
            return new DenseVector<TData>(this.FElements);
        }
    }
}
