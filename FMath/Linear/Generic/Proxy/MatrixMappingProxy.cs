using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Base.Proxy;

namespace FMath.Linear.Generic.Proxy
{
    /// <summary>
    /// Matrix to matrix proxy thath projects each element into a new format.
    /// </summary>
    /// <typeparam name="TIn">The element type of the input matrix.</typeparam>
    /// <typeparam name="TOut">The element type of the output matrix.</typeparam>
    /// <seealso cref="MatrixMatrixProxy{TOut}" />
    public class MatrixMappingProxy<TIn, TOut> :
        MatrixMatrixProxy<TOut>
    {
        private readonly Func<TIn, TOut> FForward;
        private readonly Func<TOut, TIn> FReverse;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixMappingProxy{TIn, TOut}"/> class.
        /// </summary>
        /// <param name="ABaseMatrix">The base matrix.</param>
        /// <param name="AForward">The forward mapping.</param>
        /// <param name="AReverse">The reverse mapping, or null to make the proxy immutable.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when AForward is null.</exception>
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

        /// <inheritDoc />
        public override object Clone()
        {
            return new MatrixMappingProxy<TIn, TOut>(this.BaseMatrix as IMatrix<TIn>, this.Forward, this.Reverse);
        }

        /// <inheritDoc />
        public override bool IsMutable
        {
            [Pure]
            get { return base.IsMutable && this.FReverse != null; }
        }

        /// <inheritDoc />
        public override MatrixIndices Size
        {
            [Pure]
            get { return this.BaseMatrix.Size; }
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