using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Base.Proxy;

namespace FMath.Linear.Generic.Proxy
{
    public sealed class MatrixCasterProxy<TData> :
        MatrixMatrixProxy<TData>
    {
        public MatrixCasterProxy(IMatrix ABaseMatrix)
            : base(ABaseMatrix)
        { }

        protected internal override TData DirectGet(MatrixIndices AIndices)
        {
            return (TData)this.BaseMatrix.Get(AIndices);
        }
        protected internal override void DirectSet(MatrixIndices AIndices, TData AValue)
        {
            throw new InvalidOperationException("Matrix caster proxy is not mutable.");
        }
        public override object Clone()
        {
            return new MatrixCasterProxy<TData>(this.BaseMatrix);
        }
        public override bool IsMutable
        {
            [Pure]
            get { return false; }
        }
        public override MatrixIndices Size
        {
            [Pure]
            get { return this.BaseMatrix.Size; }
        }
    }
}
