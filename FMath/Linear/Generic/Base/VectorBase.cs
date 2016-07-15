using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Base
{
    /// <summary>
    /// Abstract generic base class for vector types.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="IVector{TData}" />
    /// <seealso cref="System.IFormattable" />
    /// <seealso cref="System.IEquatable{VectorBase{TData}}" />
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
        /// <inheritDoc />
        public Type ElementType
        {
            [Pure]
            get { return typeof(TData); }
        }
        #endregion

        #region ICloneable
        /// <inheritDoc />
        [Pure]
        public abstract object Clone();
        #endregion

        #region IEnumerable
        /// <inheritDoc />
        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region IVector
        /// <inheritDoc />
        [Pure]
        object IVector.Get(int AIndex)
        {
            return this.Get(AIndex);
        }

        /// <inheritDoc />
        public int Size
        {
            [Pure]
            get { return this.FSize; }
        }

        /// <inheritDoc />
        object IVector.this[int AIndex]
        {
            [Pure]
            get { return ((IVector)this).Get(AIndex); }
        }
        #endregion

        #region IEnumerable<TData>
        /// <inheritDoc />
        [Pure]
        public virtual IEnumerator<TData> GetEnumerator()
        {
            return Enumerable.Range(0, this.FSize).Select(this.Get).GetEnumerator();
        }
        #endregion

        #region IVector<TData>
        /// <inheritDoc />
        [Pure]
        public TData Get(int AIndex)
        {
            if (!this.IsDefined(AIndex))
                throw new ArgumentOutOfRangeException("AIndex");

            return this.DirectGet(AIndex);
        }

        /// <inheritDoc />
        public TData this[int AIndex]
        {
            [Pure]
            get { return this.Get(AIndex); }
        }
        #endregion

        #region IFormattable
        /// <inheritDoc />
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
        /// <inheritDoc />
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
        /// <inheritDoc />
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
        /// <inheritDoc />
        public override int GetHashCode()
        {
            return Vector.Hash(this);
        }
        /// <inheritDoc />
        public override string ToString()
        {
            return this.ToString(null, null);
        }
        #endregion
    }
}