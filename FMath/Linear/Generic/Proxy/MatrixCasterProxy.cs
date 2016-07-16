using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Base.Proxy;

namespace FMath.Linear.Generic.Proxy
{
    /// <summary>
    /// Immutable matrix to matrix proxy that casts the elements.
    /// </summary>
    /// <typeparam name="TData">The type to cast to.</typeparam>
    /// <seealso cref="MatrixMatrixProxy{TData}" />
    public sealed class MatrixCasterProxy<TData> :
        MatrixMatrixProxy<TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixCasterProxy{TData}"/> class.
        /// </summary>
        /// <param name="ABaseMatrix">The base matrix.</param>
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

        /// <inheritDoc />
        public override object Clone()
        {
            return new MatrixCasterProxy<TData>(this.BaseMatrix);
        }

        /// <inheritDoc />
        public override bool IsMutable
        {
            [Pure]
            get { return false; }
        }

        /// <inheritDoc />
        public override MatrixIndices Size
        {
            [Pure]
            get { return this.BaseMatrix.Size; }
        }
    }
}
