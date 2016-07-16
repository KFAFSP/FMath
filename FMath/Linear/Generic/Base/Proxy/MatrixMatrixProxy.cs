using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Base.Proxy
{
    /// <summary>
    /// Abstract generic base class for matrix proxies that produce a new matrix.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="MatrixProxy" />
    /// <seealso cref="IMutableMatrix{TData}" />
    /// <seealso cref="System.IFormattable" />
    public abstract class MatrixMatrixProxy<TData> :
        MatrixProxy,
        IMutableMatrix<TData>,
        IFormattable
    {
        protected MatrixMatrixProxy(IMatrix ABaseMatrix)
            : base(ABaseMatrix)
        { }

        protected internal abstract TData DirectGet(MatrixIndices AIndices);
        protected internal abstract void DirectSet(MatrixIndices AIndices, TData AValue);

        #region IStructure
        /// <inheritDoc />
        Type IStructure.ElementType
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

        #region IMatrix
        /// <inheritDoc />
        [Pure]
        object IMatrix.Get(MatrixIndices AIndices)
        {
            return this.Get(AIndices);
        }

        /// <inheritDoc />
        public abstract MatrixIndices Size { get; }

        /// <inheritDoc />
        object IMatrix.this[int ARow, int ACol]
        {
            [Pure]
            get { return ((IMatrix)this).Get(new MatrixIndices(ARow, ACol)); }
        }
        #endregion

        #region IMatrix<TData>
        /// <inheritDoc />
        [Pure]
        public TData Get(MatrixIndices AIndices)
        {
            if (!this.AreDefined(AIndices))
                throw new ArgumentOutOfRangeException("AIndices");

            return this.DirectGet(AIndices);
        }
        #endregion

        #region IFormattable
        /// <inheritDoc />
        [Pure]
        public string ToString(string AFormat, IFormatProvider AFormatProvider)
        {
            string sCellFormat = null;
            if (AFormat != null)
            {
                int iDelim = AFormat.IndexOf(',');
                if (iDelim != -1)
                {
                    sCellFormat = AFormat.Substring(iDelim + 1);
                    AFormat = AFormat.Substring(0, iDelim);
                }
            }

            return Matrix.Format(this, AFormat, sCellFormat, AFormatProvider);
        }
        #endregion

        #region IAssignable<IMatrix>
        /// <inheritDoc />
        public void Assign(IMatrix AFrom)
        {
            if (AFrom == null)
                throw new ArgumentNullException("AFrom");

            Matrix.Copy(AFrom, this);
        }
        #endregion

        #region IMutableMatrix
        /// <inheritDoc />
        void IMutableMatrix.Set(MatrixIndices AIndices, object AData)
        {
            if (!AData.Matches<TData>())
                throw new ArgumentException("Matrix type mismatch.");

            this.Set(AIndices, (TData)AData);
        }

        /// <inheritDoc />
        object IMutableMatrix.this[int ARow, int ACol]
        {
            [Pure]
            get { return ((IMatrix)this).Get(new MatrixIndices(ARow, ACol)); }
            set { ((IMutableMatrix)this).Set(new MatrixIndices(ARow, ACol), value); }
        }
        #endregion

        #region IMutableMatrix<TData>
        /// <inheritDoc />
        public void Set(MatrixIndices AIndices, TData AData)
        {
            if (!this.IsMutable)
                throw new InvalidOperationException("Base matrix is not mutable.");
            if (!this.AreDefined(AIndices))
                throw new ArgumentOutOfRangeException("AIndices");

            this.DirectSet(AIndices, AData);
        }

        /// <inheritDoc />
        public TData this[int ARow, int ACol]
        {
            [Pure]
            get { return this.Get(new MatrixIndices(ARow, ACol)); }
            set { this.Set(new MatrixIndices(ARow, ACol), value); }
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

            if (AOther is IMatrix)
                return Matrix.AreEqual(this, (IMatrix)AOther);

            return false;
        }
        /// <inheritDoc />
        public override int GetHashCode()
        {
            return Matrix.Hash(this);
        }
        /// <inheritDoc />
        public override string ToString()
        {
            return this.ToString(null, null);
        }
        #endregion
    }
}