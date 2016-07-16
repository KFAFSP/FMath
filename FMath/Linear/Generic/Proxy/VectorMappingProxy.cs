using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Base.Proxy;

namespace FMath.Linear.Generic.Proxy
{
    public class VectorMappingProxy<TIn, TOut> :
        VectorVectorProxy<TOut>
    {
        private readonly Func<TIn, TOut> FForward;
        private readonly Func<TOut, TIn> FReverse;
        public VectorMappingProxy(
            IVector<TIn> ABaseVector,
            Func<TIn, TOut> AForward,
            Func<TOut, TIn> AReverse)
            : base(ABaseVector)
        {
            if (AForward == null)
                throw new ArgumentNullException("AForward");

            this.FForward = AForward;
            this.FReverse = AReverse;
        }

        protected internal override TOut DirectGet(int AIndex)
        {
            return this.FForward(((IVector<TIn>)this.BaseVector).Get(AIndex));
        }
        protected internal override void DirectSet(int AIndex, TOut AValue)
        {
            ((IMutableVector<TIn>)this.BaseVector).Set(AIndex, this.FReverse(AValue));
        }
        public override object Clone()
        {
            return new VectorMappingProxy<TIn, TOut>(this.BaseVector as IVector<TIn>, this.Forward, this.Reverse);
        }
        public override bool IsMutable
        {
            [Pure]
            get { return base.IsMutable && this.FReverse != null; }
        }
        public override int Size
        {
            [Pure]
            get { return this.BaseVector.Size; }
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