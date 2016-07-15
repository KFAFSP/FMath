using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Base
{
    /// <summary>
    /// Abstract generic base class for mutable vector types.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="VectorBase{TData}" />
    /// <seealso cref="IMutableVector{TData}" />
    /// <seealso cref="FMath.IAssignable{VectorBase{TData}}" />
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
        /// <inheritDoc />
        public void Assign(IVector AFrom)
        {
            if (AFrom == null)
                throw new ArgumentNullException("AFrom");

            Vector.Copy(AFrom, this);
        }
        #endregion

        #region IMutableVector
        /// <inheritDoc />
        void IMutableVector.Set(int AIndex, object AData)
        {
            if (!AData.Matches<TData>())
                throw new ArgumentException("Vector type mismatch.");

            this.Set(AIndex, (TData)AData);
        }

        /// <inheritDoc />
        object IMutableVector.this[int AIndex]
        {
            [Pure]
            get { return ((IMutableVector)this).Get(AIndex); }
            set { ((IMutableVector)this).Set(AIndex, value); }
        }
        #endregion

        #region IMutableVector<TData>
        /// <inheritDoc />
        public void Set(int AIndex, TData AData)
        {
            if (!this.IsDefined(AIndex))
                throw new ArgumentOutOfRangeException("AIndex");

            this.DirectSet(AIndex, AData);
        }

        /// <inheritDoc />
        public new TData this[int AIndex]
        {
            [Pure]
            get { return this.Get(AIndex); }
            set { this.Set(AIndex, value); }
        }
        #endregion

        #region IAssignable<VectorBase<TData>>
        /// <inheritDoc />
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