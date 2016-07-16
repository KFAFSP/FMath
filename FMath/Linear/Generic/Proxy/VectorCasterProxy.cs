using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Base.Proxy;

namespace FMath.Linear.Generic.Proxy
{
    public sealed class VectorCasterProxy<TData> :
        VectorVectorProxy<TData>
    {
        public VectorCasterProxy(IVector ABaseVector)
            : base(ABaseVector)
        { }

        protected internal override TData DirectGet(int AIndex)
        {
            return (TData)this.BaseVector.Get(AIndex);
        }
        protected internal override void DirectSet(int AIndex, TData AValue)
        {
            throw new InvalidOperationException("Vector caster proxy is not mutable.");
        }
        public override object Clone()
        {
            return new VectorCasterProxy<TData>(this.BaseVector);
        }
        public override bool IsMutable
        {
            [Pure]
            get { return false; }
        }
        public override int Size
        {
            [Pure]
            get { return this.BaseVector.Size; }
        }
    }
}