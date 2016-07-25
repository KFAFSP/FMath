using System;

using FMath.Linear.Generic.Base.Proxy;

namespace FMath.Linear.Generic.Proxy
{
    public sealed class MatrixColumnProxy<TData> :
        MatrixVectorProxy<TData>
    {
        private readonly int FColumn;

        public MatrixColumnProxy(IMatrix<TData> ABaseMatrix, int AColumn)
            : base(ABaseMatrix)
        {
            if (AColumn < 0 || AColumn >= ABaseMatrix.Size.N)
                throw new ArgumentOutOfRangeException("AColumn");

            this.FColumn = AColumn;
        }

        protected internal override TData DirectGet(int AIndex)
        {
            return ((IMatrix<TData>)this.BaseMatrix).Get(new MatrixIndices(AIndex, this.FColumn));
        }
        protected internal override void DirectSet(int AIndex, TData AValue)
        {
            ((IMutableMatrix<TData>)this.BaseMatrix).Set(new MatrixIndices(AIndex, this.FColumn), AValue);
        }

        public override object Clone()
        {
            return new MatrixColumnProxy<TData>((IMatrix<TData>)this.BaseMatrix, this.FColumn);
        }

        public override int Size
        {
            get { return this.BaseMatrix.Size.M; }
        }

        public int Column
        {
            get { return this.FColumn; }
        }
    }

    public sealed class MatrixRowProxy<TData> :
        MatrixVectorProxy<TData>
    {
        private readonly int FRow;

        public MatrixRowProxy(IMatrix<TData> ABaseMatrix, int ARow)
            : base(ABaseMatrix)
        {
            if (ARow < 0 || ARow >= ABaseMatrix.Size.M)
                throw new ArgumentOutOfRangeException("ARow");

            this.FRow = ARow;
        }

        protected internal override TData DirectGet(int AIndex)
        {
            return ((IMatrix<TData>)this.BaseMatrix).Get(new MatrixIndices(this.FRow, AIndex));
        }
        protected internal override void DirectSet(int AIndex, TData AValue)
        {
            ((IMutableMatrix<TData>)this.BaseMatrix).Set(new MatrixIndices(this.FRow, AIndex), AValue);
        }

        public override object Clone()
        {
            return new MatrixColumnProxy<TData>((IMatrix<TData>)this.BaseMatrix, this.FRow);
        }

        public override int Size
        {
            get { return this.BaseMatrix.Size.N; }
        }

        public int Row
        {
            get { return this.FRow; }
        }
    }
}
