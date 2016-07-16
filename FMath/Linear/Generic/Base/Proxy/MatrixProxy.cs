using System;

namespace FMath.Linear.Generic.Base.Proxy
{
    public abstract class MatrixProxy
    {
        private readonly IMatrix FBaseMatrix;

        protected MatrixProxy(IMatrix ABaseMatrix)
        {
            if (ABaseMatrix == null)
                throw new ArgumentNullException("ABaseMatrix");

            this.FBaseMatrix = ABaseMatrix;
        }
        public IMatrix BaseMatrix
        {
            get { return this.FBaseMatrix; }
        }
        public virtual bool IsMutable
        {
            get { return this.FBaseMatrix is IMutableMatrix; }
        }
    }
}
