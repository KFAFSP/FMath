using FMath.Linear.Generic.Base;

namespace FMath.Linear.Generic.Mutable
{
    public class DenseMatrix<TData> :
        MutableMatrixBase<TData>
    {
        protected readonly TData[,] FCells;
        public DenseMatrix(MatrixIndices ASize)
            : base(ASize)
        {
            this.FCells = new TData[ASize.M, ASize.N];
        }
        public DenseMatrix(TData[,] ACells, bool ACopy = true)
            : base(ACells != null ? new MatrixIndices(ACells.GetLength(0), ACells.GetLength(1)) : MatrixIndices.Zero)
        {
            if (ACells == null)
                this.FCells = new TData[0,0];
            else
                this.FCells = ACopy
                    ? (TData[,])ACells.Clone()
                    : ACells;
        }

        protected internal override TData DirectGet(MatrixIndices AIndices)
        {
            return this.FCells[AIndices.M, AIndices.N];
        }
        protected internal override void DirectSet(MatrixIndices AIndices, TData AData)
        {
            this.FCells[AIndices.M, AIndices.N] = AData;
        }
        public override object Clone()
        {
            return new DenseMatrix<TData>(this.FCells);
        }
    }
}