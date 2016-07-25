using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using FMath.Linear.Generic;
using FMath.Linear.Static;

namespace FMath.Linear
{
    public static class MatrixIndicesExtensions
    {
        #region Range iteration
        [Pure]
        public static IEnumerable<MatrixIndices> Range(this MatrixIndices AStart, int ARows, int ACols)
        {
            if (ARows == 0 && ACols == 0)
                yield break;

            if (ARows <= 0)
                throw new ArgumentOutOfRangeException("ARows");
            if (ACols <= 0)
                throw new ArgumentOutOfRangeException("ACols");

            for (int iRow = AStart.M; iRow < AStart.M + ARows; iRow++)
                for (int iCol = AStart.N; iCol < AStart.N + ACols; iCol++)
                    yield return new MatrixIndices(iRow, iCol);
        }
        [Pure]
        public static IEnumerable<MatrixIndices> Range(this MatrixIndices AStart, MatrixIndices ACount)
        {
            return AStart.Range(ACount.M, ACount.N);
        }
        #endregion
    }

    public struct MatrixIndices :
        IVector<int>,
        IEquatable<MatrixIndices>,
        IComparable<MatrixIndices>,
        IEquatable<Tuple<int, int>>
    {
        #region Static fields
        public static readonly MatrixIndices Zero = new MatrixIndices();
        public static readonly MatrixIndices One = new MatrixIndices(1, 1);
        #endregion

        public readonly int M, N;

        public MatrixIndices(int AM, int AN)
        {
            this.M = AM;
            this.N = AN;
        }

        #region ICloneable
        [Pure]
        public object Clone()
        {
            return new MatrixIndices(this.M, this.N);
        }
        #endregion

        #region IStructure
        Type IStructure.ElementType
        {
            [Pure]
            get { return typeof(int); }
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
            return ((IVector<int>)this).Get(AIndex);
        }
        int IVector.Size
        {
            [Pure]
            get { return 2; }
        }
        object IVector.this[int AIndex]
        {
            [Pure]
            get { return ((IVector)this).Get(AIndex); }
        }
        #endregion

        #region IEnumerable<int>
        [Pure]
        public IEnumerator<int> GetEnumerator()
        {
            return new[] { this.M, this.N }.AsEnumerable().GetEnumerator();
        }
        #endregion

        #region IVector<int>
        [Pure]
        int IVector<int>.Get(int AIndex)
        {
            if (AIndex == 0)
                return this.M;
            if (AIndex == 1)
                return this.N;

            throw new ArgumentOutOfRangeException("AIndex");
        }
        public int this[int AIndex]
        {
            [Pure]
            get { return ((IVector<int>)this).Get(AIndex); }
        }
        #endregion

        #region IEquatable<MatrixIndices>
        [Pure]
        public bool Equals(MatrixIndices AOther)
        {
            return this.M == AOther.M
                   && this.N == AOther.N;
        }
        #endregion

        #region IComparable<MatrixIndices>
        [Pure]
        public int CompareTo(MatrixIndices AOther)
        {
            return this.M != AOther.M
                ? this.M - AOther.M
                : this.N - AOther.N;
        }
        #endregion

        #region IEquatable<Tuple<int, int>>
        [Pure]
        public bool Equals(Tuple<int, int> AOther)
        {
            if (AOther == null)
                return false;

            return this.M == AOther.Item1 && this.N == AOther.Item2;
        }
        #endregion

        #region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther is MatrixIndices)
                return this.Equals((MatrixIndices)AOther);
            if (AOther is Tuple<int, int>)
                return this.Equals((Tuple<int, int>)AOther);
            if (AOther is IVector)
                return Vector.AreEqual(this, (IVector)AOther);

            return false;
        }
        public override int GetHashCode()
        {
            return this.M * Vector.C_HashSaltPrime + this.N;
        }
        public override string ToString()
        {
            return string.Format("[{0} {1}]", this.M, this.N);
        }
        #endregion

        #region Static operator overloads
        public static bool operator ==(MatrixIndices ALeft, MatrixIndices ARight)
        {
            return ALeft.Equals(ARight);
        }
        public static bool operator !=(MatrixIndices ALeft, MatrixIndices ARight)
        {
            return !ALeft.Equals(ARight);
        }

        public static bool operator >(MatrixIndices ALeft, MatrixIndices ARight)
        {
            return ALeft.CompareTo(ARight) > 0;
        }
        public static bool operator >=(MatrixIndices ALeft, MatrixIndices ARight)
        {
            return ALeft.CompareTo(ARight) >= 0;
        }
        public static bool operator <(MatrixIndices ALeft, MatrixIndices ARight)
        {
            return ALeft.CompareTo(ARight) < 0;
        }
        public static bool operator <=(MatrixIndices ALeft, MatrixIndices ARight)
        {
            return ALeft.CompareTo(ARight) <= 0;
        }

        public static MatrixIndices operator -(MatrixIndices ALeft)
        {
            return new MatrixIndices(-ALeft.M, -ALeft.N);
        }
        public static MatrixIndices operator +(MatrixIndices ALeft, MatrixIndices ARight)
        {
            return new MatrixIndices(ALeft.M + ARight.M, ALeft.N + ARight.N);
        }
        public static MatrixIndices operator -(MatrixIndices ALeft, MatrixIndices ARight)
        {
            return new MatrixIndices(ALeft.M - ARight.M, ALeft.N - ARight.N);
        }
        public static MatrixIndices operator *(MatrixIndices ALeft, int ARight)
        {
            return new MatrixIndices(ALeft.M * ARight, ALeft.N * ARight);
        }
        #endregion
    }
}
