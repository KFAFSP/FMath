using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Immutable
{
    public struct ArrayMatrix<TData> :
        IMatrix<TData>,
        IFormattable,
        IEquatable<ArrayMatrix<TData>>
    {
        private readonly TData[,] FElements;
        public ArrayMatrix(
            TData[,] AElements,
            bool ACopy = true)
        {
            this.FElements = ACopy
                ? (TData[,])AElements.Clone()
                : AElements;
        }

        #region IStructure
        public Type ElementType
        {
            [Pure]
            get { return typeof(TData); }
        }
        #endregion

        #region ICloneable
        [Pure]
        public object Clone()
        {
            return new ArrayMatrix<TData>(this.FElements);
        }
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
            get
            {
                return this.FElements == null
                    ? MatrixIndices.Zero
                    : new MatrixIndices(this.FElements.GetLength(0), this.FElements.GetLength(1));
            }
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

            return this.FElements[AIndices.M, AIndices.N];
        }
        public TData this[int ARow, int ACol]
        {
            [Pure]
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

        #region IEquatable<ArrayMatrix<TData>>
        [Pure]
        public bool Equals(ArrayMatrix<TData> AOther)
        {
            if (this.FElements == null && AOther.FElements == null)
                return true;

            if (this.FElements == null || AOther.FElements == null)
                return false;

            if (AOther.FElements.GetLength(0) != this.FElements.GetLength(0) ||
                AOther.FElements.GetLength(1) != this.FElements.GetLength(1))
                return false;

            for (int M = 0; M < this.FElements.GetLength(0); M++)
                for (int N = 0; N < this.FElements.GetLength(1); N++)
                    if (!this.FElements[M, N].Equals(AOther.FElements[M, N]))
                        return false;

            return true;
        }
        #endregion

        #region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther == null)
                return false;
            if (AOther is ArrayMatrix<TData>)
                return this.Equals((ArrayMatrix<TData>)AOther);
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

        #region Static casting operators
        public static explicit operator ArrayMatrix<TData>(TData[,] AArray)
        {
            return new ArrayMatrix<TData>(AArray, false);
        }
        public static explicit operator TData[,](ArrayMatrix<TData> AMatrix)
        {
            return AMatrix.FElements;
        }
        #endregion

        #region Static operator overloads
        public static bool operator ==(ArrayMatrix<TData> ALeft, IMatrix<TData> ARight)
        {
            return ALeft != null
                ? ALeft.Equals(ARight)
                : Matrix.Equals(ARight, ALeft);
        }
        public static bool operator !=(ArrayMatrix<TData> ALeft, IMatrix<TData> ARight)
        {
            return !(ALeft == ARight);
        }
        #endregion
    }
}
