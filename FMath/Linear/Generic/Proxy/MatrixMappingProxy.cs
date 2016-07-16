using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Base.Proxy;

namespace FMath.Linear.Generic.Proxy
{
    public class MatrixMappingProxy<TIn, TOut> :
        MatrixMatrixProxy<TOut>
    {
        private readonly Func<TIn, TOut> FForward;
        private readonly Func<TOut, TIn> FReverse;
        public MatrixMappingProxy(
            IMatrix<TIn> ABaseMatrix,
            Func<TIn, TOut> AForward,
            Func<TOut, TIn> AReverse)
            : base(ABaseMatrix)
        {
            if (AForward == null)
                throw new ArgumentNullException("AForward");

            this.FForward = AForward;
            this.FReverse = AReverse;
        }

        protected internal override TOut DirectGet(MatrixIndices AIndices)
        {
            return this.FForward(((IMatrix<TIn>)this.BaseMatrix).Get(AIndices));
        }
        protected internal override void DirectSet(MatrixIndices AIndices, TOut AValue)
        {
            ((IMutableMatrix<TIn>)this.BaseMatrix).Set(AIndices, this.FReverse(AValue));
        }
        public override object Clone()
        {
            return new MatrixMappingProxy<TIn, TOut>(this.BaseMatrix as IMatrix<TIn>, this.Forward, this.Reverse);
        }
        public override bool IsMutable
        {
            [Pure]
            get { return base.IsMutable && this.FReverse != null; }
        }
        public override MatrixIndices Size
        {
            [Pure]
            get { return this.BaseMatrix.Size; }
        }
        public Func<TIn, TOut> Forward
        {
            get { return this.FForward; }
        }
        public Func<TOut, TIn> Reverse
        {
            get { return this.FReverse; }
        }
    }
}