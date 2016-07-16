using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Base.Proxy;

namespace FMath.Linear.Generic.Proxy
{
    /// <summary>
    /// Vector to vector proxy that projects each element into a new format.
    /// </summary>
    /// <typeparam name="TIn">The type of the in.</typeparam>
    /// <typeparam name="TOut">The type of the out.</typeparam>
    /// <seealso cref="FMath.Linear.Generic.Base.Proxy.VectorVectorProxy{TOut}" />
    public class VectorMappingProxy<TIn, TOut> :
        VectorVectorProxy<TOut>
    {
        private readonly Func<TIn, TOut> FForward;
        private readonly Func<TOut, TIn> FReverse;

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorMappingProxy{TIn, TOut}"/> class.
        /// </summary>
        /// <param name="ABaseVector">The base vector.</param>
        /// <param name="AForward">The forward mapping.</param>
        /// <param name="AReverse">The reverse mapping, or null to make the vector immutable.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when AForward is null.</exception>
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

        /// <inheritDoc />
        public override object Clone()
        {
            return new VectorMappingProxy<TIn, TOut>(this.BaseVector as IVector<TIn>, this.Forward, this.Reverse);
        }

        /// <inheritDoc />
        public override bool IsMutable
        {
            [Pure]
            get { return base.IsMutable && this.FReverse != null; }
        }

        /// <inheritDoc />
        public override int Size
        {
            [Pure]
            get { return this.BaseVector.Size; }
        }

        /// <summary>
        /// Gets the forward mapping.
        /// </summary>
        /// <value>
        /// The forward mapping.
        /// </value>
        public Func<TIn, TOut> Forward
        {
            get { return this.FForward; }
        }
        /// <summary>
        /// Gets the reverse mapping.
        /// </summary>
        /// <value>
        /// The reverse mapping.
        /// </value>
        public Func<TOut, TIn> Reverse
        {
            get { return this.FReverse; }
        }
    }
}