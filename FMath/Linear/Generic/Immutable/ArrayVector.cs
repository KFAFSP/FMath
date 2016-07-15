using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Immutable
{
    /// <summary>
    /// Immutable vector type.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="IVector{TData}" />
    /// <seealso cref="System.IFormattable" />
    /// <seealso cref="System.IEquatable{ArrayVector{TData}}" />
    public struct ArrayVector<TData> :
        IVector<TData>,
        IFormattable,
        IEquatable<ArrayVector<TData>>
    {
        private readonly TData[] FElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayVector{TData}"/> struct.
        /// </summary>
        /// <param name="AElements">The array of elements.</param>
        /// <param name="ACopy">If set to <c>true</c> the array will be flat-copied, otherwise the array will be assigned by reference.</param>
        public ArrayVector(
            TData[] AElements,
            bool ACopy = true)
        {
            this.FElements = ACopy
                ? (TData[])AElements.Clone()
                : AElements;
        }

        #region IStructure
        /// <inheritdoc />
        public Type ElementType
        {
            [Pure]
            get { return typeof(TData); }
        }
        #endregion

        #region ICloneable
        /// <inheritdoc />
        [Pure]
        public object Clone()
        {
            return new ArrayVector<TData>(this.FElements);
        }
        #endregion

        #region IEnumerable
        /// <inheritdoc />
        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region IVector
        /// <inheritdoc />
        [Pure]
        object IVector.Get(int AIndex)
        {
            return this.Get(AIndex);
        }

        /// <inheritdoc />
        public int Size
        {
            [Pure]
            get { return this.FElements == null ? 0 : this.FElements.Length; }
        }

        /// <inheritdoc />
        object IVector.this[int AIndex]
        {
            [Pure]
            get { return ((IVector)this).Get(AIndex); }
        }
        #endregion

        #region IEnumerable<TData>
        /// <inheritdoc />
        [Pure]
        public IEnumerator<TData> GetEnumerator()
        {
            return this.FElements == null
                ? Enumerable.Empty<TData>().GetEnumerator()
                : this.FElements.AsEnumerable().GetEnumerator();
        }
        #endregion

        #region IVector<TData>
        /// <inheritdoc />
        [Pure]
        public TData Get(int AIndex)
        {
            if (!this.IsDefined(AIndex))
                throw new ArgumentOutOfRangeException("AIndex");

            return this.FElements[AIndex];
        }

        /// <inheritdoc />
        public TData this[int AIndex]
        {
            [Pure]
            get { return this.Get(AIndex); }
        }
        #endregion

        #region IFormattable
        /// <inheritdoc />
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
        /// <inheritdoc />
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
        /// <inheritdoc />
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
        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Vector.Hash(this);
        }
        /// <inheritdoc />
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
