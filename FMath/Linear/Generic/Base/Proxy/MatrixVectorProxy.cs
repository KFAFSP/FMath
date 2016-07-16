using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Base.Proxy
{
    /// <summary>
    /// Abstract generic base class for matrix proxies that produce a new vector.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="MatrixProxy" />
    /// <seealso cref="IMutableVector{TData}" />
    /// <seealso cref="System.IFormattable" />
    public abstract class MatrixVectorProxy<TData> :
        MatrixProxy,
        IMutableVector<TData>,
        IFormattable
    {
        protected MatrixVectorProxy(IMatrix ABaseMatrix)
            : base(ABaseMatrix)
        { }

        protected internal abstract TData DirectGet(int AIndex);
        protected internal abstract void DirectSet(int AIndex, TData AValue);

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
        public abstract int Size { get; }

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
            return Enumerable.Range(0, this.Size).Select(this.Get).GetEnumerator();
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
            if (!this.IsMutable)
                throw new InvalidOperationException("Base matrix is not mutable.");
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

        #region System.Object overrides
        /// <inheritDoc />
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