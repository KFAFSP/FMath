using System;

namespace FMath.Linear.Generic.Base.Proxy
{
    /// <summary>
    /// Abstract base class for proxies that operate on matrices.
    /// </summary>
    public abstract class MatrixProxy
    {
        private readonly IMatrix FBaseMatrix;

        protected MatrixProxy(IMatrix ABaseMatrix)
        {
            if (ABaseMatrix == null)
                throw new ArgumentNullException("ABaseMatrix");

            this.FBaseMatrix = ABaseMatrix;
        }

        /// <summary>
        /// Gets the underlying base matrix.
        /// </summary>
        /// <value>
        /// The underlying base matrix.
        /// </value>
        public IMatrix BaseMatrix
        {
            get { return this.FBaseMatrix; }
        }

        /// <summary>
        /// Gets a value indicating whether this matrix is mutable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this matrix is mutable, <c>false</c> otherwise.
        /// </value>
        public bool IsMutable
        {
            get { return this.FBaseMatrix is IMutableMatrix; }
        }
    }
}
