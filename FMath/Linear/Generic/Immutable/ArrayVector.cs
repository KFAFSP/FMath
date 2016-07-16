using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Immutable
{
    public struct ArrayVector<TData> :
        IVector<TData>,
        IFormattable,
        IEquatable<ArrayVector<TData>>
    {
        private readonly TData[] FElements;
        public ArrayVector(
            TData[] AElements,
            bool ACopy = true)
        {
            this.FElements = ACopy
                ? (TData[])AElements.Clone()
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
            return new ArrayVector<TData>(this.FElements);
        }
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
            get { return this.FElements == null ? 0 : this.FElements.Length; }
        }
        object IVector.this[int AIndex]
        {
            [Pure]
            get { return ((IVector)this).Get(AIndex); }
        }
        #endregion

        #region IEnumerable<TData>
        [Pure]
        public IEnumerator<TData> GetEnumerator()
        {
            return this.FElements == null
                ? Enumerable.Empty<TData>().GetEnumerator()
                : this.FElements.AsEnumerable().GetEnumerator();
        }
        #endregion

        #region IVector<TData>
        [Pure]
        public TData Get(int AIndex)
        {
            if (!this.IsDefined(AIndex))
                throw new ArgumentOutOfRangeException("AIndex");

            return this.FElements[AIndex];
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

        #region IEquatable<ArrayVector<TData>>
        [Pure]
        public bool Equals(ArrayVector<TData> AOther)
        {
            if (this.FElements == null && AOther.FElements == null)
                return true;

            if (this.FElements == null || AOther.FElements == null)
                return false;

            if (AOther.FElements.Length != this.FElements.Length)
                return false;

            for (int I = 0; I < this.FElements.Length; I++)
                if (!this.FElements[I].Equals(AOther.FElements[I]))
                    return false;

            return true;
        }
        #endregion

        #region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther == null)
                return false;
            if (AOther is ArrayVector<TData>)
                return this.Equals((ArrayVector<TData>)AOther);
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

        #region Static casting operators
        public static explicit operator ArrayVector<TData>(TData[] AArray)
        {
            return new ArrayVector<TData>(AArray, false);
        }
        public static explicit operator TData[](ArrayVector<TData> AVector)
        {
            return AVector.FElements;
        }
        #endregion

        #region Static operator overloads
        public static bool operator ==(ArrayVector<TData> ALeft, IVector<TData> ARight)
        {
            return ALeft != null
                ? ALeft.Equals(ARight)
                : Vector.Equals(ARight, ALeft);
        }
        public static bool operator !=(ArrayVector<TData> ALeft, IVector<TData> ARight)
        {
            return !(ALeft == ARight);
        }
        #endregion
    }
}
