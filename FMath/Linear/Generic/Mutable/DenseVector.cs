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
        private readonly TData[] FElements;

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
