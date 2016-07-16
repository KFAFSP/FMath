using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Base.Proxy
{
    public abstract class VectorVectorProxy<TData> :
        VectorProxy,
        IMutableVector<TData>,
        IFormattable
    {
        protected VectorVectorProxy(IVector ABaseVector)
            : base(ABaseVector)
        { }

        protected internal abstract TData DirectGet(int AIndex);
        protected internal abstract void DirectSet(int AIndex, TData AValue);

        #region IStructure
        public Type ElementType
        {
            [Pure]
            get { return typeof(TData); }
        }
        #endregion

        #region ICloneable
        [Pure]
        public abstract object Clone();
        #endregion

        #region IEnumerable
        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region IVector
        [Pure]
        object IVector.Get(int AIndex)
        {
            return this.Get(AIndex);
        }
        public abstract int Size { get; }
        object IVector.this[int AIndex]
        {
            [Pure]
            get { return ((IVector)this).Get(AIndex); }
        }
        #endregion

        #region IEnumerable<TData>
        [Pure]
        public virtual IEnumerator<TData> GetEnumerator()
        {
            return Enumerable.Range(0, this.Size).Select(this.Get).GetEnumerator();
        }
        #endregion

        #region IVector<TData>
        [Pure]
        public TData Get(int AIndex)
        {
            if (!this.IsDefined(AIndex))
                throw new ArgumentOutOfRangeException("AIndex");

            return this.DirectGet(AIndex);
        }
        #endregion

        #region IFormattable
        [Pure]
        public string ToString(string AFormat, IFormatProvider AFormatProvider)
        {
            string sElementFormat = null;
            if (AFormat != null)
            {
                int iDelim = AFormat.IndexOf(',');
                if (iDelim != -1)
                {
                    sElementFormat = AFormat.Substring(iDelim + 1);
                    AFormat = AFormat.Substring(0, iDelim);
                }
            }

            return Vector.Format(this, AFormat, sElementFormat, AFormatProvider);
        }
        #endregion

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
            if (!this.IsMutable)
                throw new InvalidOperationException("Base matrix is not mutable.");
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

        #region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther == null)
                return false;

            if (object.ReferenceEquals(AOther, this))
                return true;

            if (AOther is IVector)
                return Vector.AreEqual(this, (IVector)AOther);

            return false;
        }
        public override int GetHashCode()
        {
            return Vector.Hash(this);
        }
        public override string ToString()
        {
            return this.ToString(null, null);
        }
        #endregion
    }
}