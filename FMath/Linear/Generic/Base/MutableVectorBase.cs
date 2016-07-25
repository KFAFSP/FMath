using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Base
{
    public abstract class MutableVectorBase<TData> :
        VectorBase<TData>,
        IMutableVector<TData>,
        IAssignable<VectorBase<TData>>
    {
        protected MutableVectorBase(int ASize)
            : base(ASize)
        { }

        protected internal abstract void DirectSet(int AIndex, TData AData);

        #region IAssignable<IVector>
        public void Assign(IVector AFrom)
        {
            if (AFrom == null)
                throw new ArgumentNullException("AFrom");

            Vector.Copy(AFrom, this);
        }
        #endregion

        #region IMutableVector
        void IMutableVector.Set(int AIndex, object AData)
        {
            if (!AData.Matches<TData>())
                throw new ArgumentException("Vector type mismatch.");

            this.Set(AIndex, (TData)AData);
        }
        object IMutableVector.this[int AIndex]
        {
            [Pure]
            get { return ((IMutableVector)this).Get(AIndex); }
            set { ((IMutableVector)this).Set(AIndex, value); }
        }
        #endregion

        #region IMutableVector<TData>
        public void Set(int AIndex, TData AData)
        {
            if (!this.IsDefined(AIndex))
                throw new ArgumentOutOfRangeException("AIndex");

            this.DirectSet(AIndex, AData);
        }
        public new TData this[int AIndex]
        {
            [Pure]
            get { return this.Get(AIndex); }
            set { this.Set(AIndex, value); }
        }
        #endregion

        #region IAssignable<VectorBase<TData>>
        public void Assign(VectorBase<TData> AOther)
        {
            if (AOther == null)
                throw new ArgumentNullException("AOther");

            if (this.Size != AOther.Size)
                throw new ArgumentException("Vector dimensions do not match.");

            for (int I = 0; I < this.Size; I++)
                this.DirectSet(I, AOther.DirectGet(I));
        }
        #endregion
    }
}