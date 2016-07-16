using System.Collections.Generic;
using System.Linq;

using FMath.Linear.Generic.Base;
using FMath.Linear.Static;

namespace FMath.Linear.Generic.Mutable
{
    public class SparseVector<TData> :
        MutableVectorBase<TData>
    {
        private readonly TData FDefault;
        private readonly IEqualityComparer<TData> FComparer;

        protected readonly SortedList<int, TData> FElements;
        public SparseVector(
            int ASize,
            TData ADefault = default(TData),
            IEqualityComparer<TData> AComparer = null,
            IDictionary<int, TData> AElements = null)
            : base(ASize)
        {
            if (AComparer == null)
                AComparer = EqualityComparer<TData>.Default;

            this.FDefault = ADefault;
            this.FComparer = AComparer;

            this.FElements = new SortedList<int, TData>();

            if (AElements != null)
                foreach (KeyValuePair<int, TData> kvpPair in AElements
                    .Where(APair => !AComparer.Equals(APair.Value, ADefault) && this.IsDefined(APair.Key)))
                    this.FElements.Add(kvpPair.Key, kvpPair.Value);
        }

        protected internal override TData DirectGet(int AIndex)
        {
            TData tOut;
            if (!this.FElements.TryGetValue(AIndex, out tOut))
                return this.FDefault;

            return tOut;
        }
        protected internal override void DirectSet(int AIndex, TData AData)
        {
            if (this.Comparer.Equals(AData, this.FDefault))
                this.FElements.Remove(AIndex);
            else
                this.FElements[AIndex] = AData;
        }
        public override object Clone()
        {
            return new SparseVector<TData>(this.Size, this.Default, this.Comparer, this.FElements);
        }
        public TData Default
        {
            get { return this.FDefault; }
        }
        public IEqualityComparer<TData> Comparer
        {
            get { return this.FComparer; }
        }
    }
}