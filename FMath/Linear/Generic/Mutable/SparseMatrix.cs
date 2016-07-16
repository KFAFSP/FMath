using System.Collections.Generic;
using System.Linq;

using FMath.Linear.Generic.Base;
using FMath.Linear.Static;

namespace FMath.Linear.Generic.Mutable
{
    public class SparseMatrix<TData> :
        MutableMatrixBase<TData>
    {
        private readonly TData FDefault;
        private readonly IEqualityComparer<TData> FComparer;

        protected readonly SortedList<MatrixIndices, TData> FCells;
        public SparseMatrix(
            MatrixIndices ASize,
            TData ADefault = default(TData),
            IEqualityComparer<TData> AComparer = null,
            IDictionary<MatrixIndices, TData> ACells = null)
            : base(ASize)
        {
            if (AComparer == null)
                AComparer = EqualityComparer<TData>.Default;

            this.FDefault = ADefault;
            this.FComparer = AComparer;

            this.FCells = new SortedList<MatrixIndices, TData>();

            if (ACells != null)
                foreach (KeyValuePair<MatrixIndices, TData> kvpPair in ACells
                    .Where(APair => !AComparer.Equals(APair.Value, ADefault) && this.AreDefined(APair.Key)))
                    this.FCells.Add(kvpPair.Key, kvpPair.Value);
        }

        protected internal override TData DirectGet(MatrixIndices AIndices)
        {
            TData tOut;
            if (!this.FCells.TryGetValue(AIndices, out tOut))
                return this.FDefault;

            return tOut;
        }
        protected internal override void DirectSet(MatrixIndices AIndices, TData AData)
        {
            if (this.Comparer.Equals(AData, this.Default))
                this.FCells.Remove(AIndices);
            else
                this.FCells[AIndices] = AData;
        }
        public override object Clone()
        {
            return new SparseMatrix<TData>(this.Size, this.Default, this.Comparer, this.FCells);
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