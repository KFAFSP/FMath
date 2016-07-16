using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Base
{
    public abstract class VectorBase<TData> :
        IVector<TData>,
        IFormattable,
        IEquatable<VectorBase<TData>>
    {
        private readonly int FSize;

        protected VectorBase(int ASize)
        {
            if (ASize < 0)
                throw new ArgumentOutOfRangeException("ASize");

            this.FSize = ASize;
        }

        [Pure]
        protected internal abstract TData DirectGet(int AIndex);

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
        public int Size
        {
            [Pure]
            get { return this.FSize; }
        }
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
            return Enumerable.Range(0, this.FSize).Select(this.Get).GetEnumerator();
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
        public TData this[int AIndex]
        {
            [Pure]
            get { return this.Get(AIndex); }
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

        #region IEquatable<VectorBase<TData>>
        [Pure]
        public bool Equals(VectorBase<TData> AOther)
        {
            if (AOther == null)
                return false;

            if (this.FSize != AOther.FSize)
                return false;

            for (int I = 0; I < this.FSize; I++)
                if (!this.DirectGet(I).Equals(AOther.DirectGet(I)))
                    return false;

            return true;
        }
        #endregion

        #region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther == null)
                return false;

            if (object.ReferenceEquals(AOther, this))
                return true;

            if (AOther is VectorBase<TData>)
                return this.Equals((VectorBase<TData>)AOther);

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