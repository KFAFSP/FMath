using System;

namespace FMath.Linear.Generic.Base.Proxy
{
    public abstract class VectorProxy
    {
        private readonly IVector FBaseVector;

        protected VectorProxy(IVector ABaseVector)
        {
            if (ABaseVector == null)
                throw new ArgumentNullException("ABaseVector");

            this.FBaseVector = ABaseVector;
        }
        public IVector BaseVector
        {
            get { return this.FBaseVector; }
        }
        public virtual bool IsMutable
        {
            get { return this.FBaseVector is IMutableVector; }
        }
    }
}