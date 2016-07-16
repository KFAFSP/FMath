using System;

namespace FMath.Linear.Generic.Base.Proxy
{
    /// <summary>
    /// Abstract base class for proxeis that operate on vectors.
    /// </summary>
    public abstract class VectorProxy
    {
        private readonly IVector FBaseVector;

        protected VectorProxy(IVector ABaseVector)
        {
            if (ABaseVector == null)
                throw new ArgumentNullException("ABaseVector");

            this.FBaseVector = ABaseVector;
        }

        /// <summary>
        /// Gets the underlying base vector.
        /// </summary>
        /// <value>
        /// The underlying base vector.
        /// </value>
        public IVector BaseVector
        {
            get { return this.FBaseVector; }
        }

        /// <summary>
        /// Gets a value indicating whether this vector is mutable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this vector is mutable, <c>false</c> otherwise.
        /// </value>
        public bool IsMutable
        {
            get { return this.FBaseVector is IMutableVector; }
        }
    }
}