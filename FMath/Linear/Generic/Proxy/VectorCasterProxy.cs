using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Base.Proxy;

namespace FMath.Linear.Generic.Proxy
{
    /// <summary>
    /// Immutable vector to vector proxy thath casts the elements.
    /// </summary>
    /// <typeparam name="TData">The type to cast to.</typeparam>
    /// <seealso cref="FMath.Linear.Generic.Base.Proxy.VectorVectorProxy{TData}" />
    public sealed class VectorCasterProxy<TData> :
        VectorVectorProxy<TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VectorCasterProxy{TData}"/> class.
        /// </summary>
        /// <param name="ABaseVector">The base vector.</param>
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

        /// <inheritDoc />
        public override object Clone()
        {
            return new VectorCasterProxy<TData>(this.BaseVector);
        }

        /// <inheritDoc />
        public override bool IsMutable
        {
            [Pure]
            get { return false; }
        }

        /// <inheritDoc />
        public override int Size
        {
            [Pure]
            get { return this.BaseVector.Size; }
        }
    }
}