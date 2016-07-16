using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Base
{
    public abstract class MatrixBase<TData> :
        IMatrix<TData>,
        IFormattable,
        IEquatable<MatrixBase<TData>>
    {
        private readonly MatrixIndices FSize;

        protected MatrixBase(MatrixIndices ASize)
        {
            if (ASize.M < 0 || ASize.N < 0)
                throw new ArgumentOutOfRangeException("ASize");
            if ((ASize.M == 0 && ASize.N != 0) || (ASize.M != 0 && ASize.N == 0))
                throw new ArgumentException("Matrix size may not be partially zero.");

            this.FSize = ASize;
        }

        protected internal abstract TData DirectGet(MatrixIndices AIndices);

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

        #region IMatrix
        [Pure]
        object IMatrix.Get(MatrixIndices AIndices)
        {
            return this.Get(AIndices);
        }
        public MatrixIndices Size
        {
            [Pure]
            get { return this.FSize; }
        }
        object IMatrix.this[int ARow, int ACol]
        {
            [Pure]
            get { return ((IMatrix)this).Get(new MatrixIndices(ARow, ACol)); }
        }
        #endregion

        #region IMatrix<TData>
        [Pure]
        public TData Get(MatrixIndices AIndices)
        {
            if (!this.AreDefined(AIndices))
                throw new ArgumentOutOfRangeException("AIndices");

            return this.DirectGet(AIndices);
        }
        [Pure]
        public TData this[int ARow, int ACol]
        {
            get { return this.Get(new MatrixIndices(ARow, ACol)); }
        }
        #endregion

        #region IFormattable
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

        #region IEquatable<MatrixBase<TData>>
        [Pure]
        public bool Equals(MatrixBase<TData> AOther)
        {
            if (AOther == null)
                return false;

            if (this.FSize != AOther.FSize)
                return false;

            for (int M = 0; M < this.FSize.M; M++)
                for (int N = 0; N < this.FSize.N; N++)
                {
                    MatrixIndices miIndices = new MatrixIndices(M, N);
                    if (!this.DirectGet(miIndices).Equals(AOther.DirectGet(miIndices)))
                        return false;
                }

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

            if (AOther is MatrixBase<TData>)
                return this.Equals((MatrixBase<TData>)AOther);

            if (AOther is IMatrix)
                return Matrix.AreEqual(this, (IMatrix)AOther);

            return false;
        }
        public override int GetHashCode()
        {
            return Matrix.Hash(this);
        }
        public override string ToString()
        {
            return this.ToString(null, null);
        }
        #endregion
    }
}
