using FMath.Linear.Generic.Base;

namespace FMath.Linear.Generic.Mutable
{
    public class DenseVector<TData> :
        MutableVectorBase<TData>
    {
        protected readonly TData[] FElements;
        public DenseVector(int ASize)
            : base(ASize)
        {
            this.FElements = new TData[3];
        }
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
        public override object Clone()
        {
            return new DenseVector<TData>(this.FElements);
        }
    }
}
