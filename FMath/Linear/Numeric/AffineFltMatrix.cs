using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic;
using FMath.Linear.Static;

namespace FMath.Linear.Numeric
{
    public sealed class AffineFltMatrix :
        IMutableMatrix<float>,
        IEquatable<AffineFltMatrix>,
        IAssignable<AffineFltMatrix>,
        IFormattable
    {
        #region Static factories
        public static AffineFltMatrix Zero { get { return new AffineFltMatrix(HomogeneousFltMatrix.Zero, Vector3Flt.Zero); } }
        public static AffineFltMatrix Identitiy { get { return new AffineFltMatrix(HomogeneousFltMatrix.Identitiy, Vector3Flt.Zero); } }
        #endregion

        #region Pure static operators
        [Pure]
        public static float Determinant(AffineFltMatrix AMatrix)
        {
            return HomogeneousFltMatrix.Determinant(AMatrix.FHomogeneous);
        }
        [Pure]
        public static AffineFltMatrix Project(AffineFltMatrix ALeft, AffineFltMatrix ARight)
        {
            return new AffineFltMatrix(ALeft.FHomogeneous * ARight.FHomogeneous, ALeft.FHomogeneous * ARight.FAffine + ALeft.FAffine);
        }
        [Pure]
        public static Vector3Flt Project(AffineFltMatrix ALeft, Vector3Flt ARight)
        {
            return HomogeneousFltMatrix.Project(ALeft.FHomogeneous, ARight).Add(ALeft.Affine);
        }
        [Pure]
        public static AffineFltMatrix Invert(AffineFltMatrix ALeft)
        {
            HomogeneousFltMatrix hfmInverted = HomogeneousFltMatrix.Invert(ALeft.FHomogeneous);
            return new AffineFltMatrix(hfmInverted, HomogeneousFltMatrix.Project(HomogeneousFltMatrix.Negate(hfmInverted), ALeft.Affine));
        }
        #endregion

        private readonly HomogeneousFltMatrix FHomogeneous;
        private readonly Vector3Flt FAffine;

        public AffineFltMatrix()
        {
            this.FHomogeneous = HomogeneousFltMatrix.Zero;
            this.FAffine = Vector3Flt.Zero;
        }
        public AffineFltMatrix(HomogeneousFltMatrix AHomogeneous, Vector3Flt AAffine)
        {
            if (AHomogeneous == null)
                throw new ArgumentNullException("AHomogeneous");
            if (AAffine == null)
                throw new ArgumentNullException("AAffine");

            this.FHomogeneous = AHomogeneous;
            this.FAffine = AAffine;
        }

        #region IStructure
        public Type ElementType
        {
            [Pure]
            get { return typeof(float); }
        }
        #endregion

        #region ICloneable
        [Pure]
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion

        [Pure]
        public AffineFltMatrix Clone()
        {
            return new AffineFltMatrix(this.FHomogeneous.Clone(), this.FAffine.Clone());
        }

        #region IMatrix
        [Pure]
        object IMatrix.Get(MatrixIndices AIndices)
        {
            return this.Get(AIndices);
        }
        public MatrixIndices Size
        {
            [Pure]
            get { return new MatrixIndices(4, 4); }
        }
        object IMatrix.this[int ARow, int ACol]
        {
            [Pure]
            get { return ((IMatrix)this).Get(new MatrixIndices(ARow, ACol)); }
        }
        #endregion

        #region IMatrix<float>
        [Pure]
        public float Get(MatrixIndices AIndices)
        {
            if (!this.AreDefined(AIndices))
                throw new ArgumentOutOfRangeException("AIndices");

            if (AIndices.M == 3)
                return AIndices.N == 3
                    ? 1.0f
                    : 0.0f;

            if (AIndices.N == 3)
                return this.FAffine.DirectGet(AIndices.M);

            return this.FHomogeneous.DirectGet(AIndices);
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

        #region IEquatable<AffineFltMatrix>
        [Pure]
        public bool Equals(AffineFltMatrix AOther)
        {
            if (AOther == null)
                return false;

            return this.FHomogeneous.Equals(AOther.FHomogeneous)
                   && this.FAffine.Equals(AOther.FAffine);
        }
        #endregion

        #region IAssignable<IMatrix>
        public void Assign(IMatrix AFrom)
        {
            if (AFrom == null)
                throw new ArgumentNullException("AFrom");

            Matrix.Copy(AFrom, this);
        }
        #endregion

        #region IMutableMatrix
        void IMutableMatrix.Set(MatrixIndices AIndices, object AData)
        {
            if (!AData.Matches<float>())
                throw new ArgumentException("Matrix type mismatch.");

            this.Set(AIndices, (float)AData);
        }
        object IMutableMatrix.this[int ARow, int ACol]
        {
            [Pure]
            get { return ((IMatrix)this).Get(new MatrixIndices(ARow, ACol)); }
            set { ((IMutableMatrix)this).Set(new MatrixIndices(ARow, ACol), value); }
        }
        #endregion

        #region IMutableMatrix<float>
        public void Set(MatrixIndices AIndices, float AData)
        {
            if (!this.AreDefined(AIndices))
                throw new ArgumentOutOfRangeException("AIndices");

            if (AIndices.M == 3)
            {
                if (!(AIndices.N == 3 && AData == 1.0f) && AData != 0.0f)
                    throw new InvalidOperationException("Affine matrix is not mutable for M = 4.");
                return;
            }

            if (AIndices.N == 3)
                this.FAffine.DirectSet(AIndices.M, AData);
            else
                this.FHomogeneous.DirectSet(AIndices, AData);
        }
        public new float this[int ARow, int ACol]
        {
            [Pure]
            get { return this.Get(new MatrixIndices(ARow, ACol)); }
            set { this.Set(new MatrixIndices(ARow, ACol), value); }
        }
        #endregion

        #region IAssignable<AffineFltMatrix>
        public void Assign(AffineFltMatrix AFrom)
        {
            if (AFrom == null)
                throw new ArgumentNullException("AFrom");

            this.FHomogeneous.Assign(AFrom.FHomogeneous);
            this.FAffine.Assign(AFrom.FAffine);
        }
        #endregion

        #region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther == null)
                return false;

            if (object.ReferenceEquals(AOther, this))
                return true;

            if (AOther is AffineFltMatrix)
                return this.Equals((AffineFltMatrix)AOther);

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

        public HomogeneousFltMatrix Homogeneous
        {
            get { return this.FHomogeneous; }
        }
        public Vector3Flt Affine
        {
            get { return this.FAffine; }
        }

        #region Static operator overloads
        public static bool operator ==(AffineFltMatrix ALeft, AffineFltMatrix ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(AffineFltMatrix ALeft, AffineFltMatrix ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static AffineFltMatrix operator *(AffineFltMatrix ALeft, AffineFltMatrix ARight)
        {
            return AffineFltMatrix.Project(ALeft, ARight);
        }
        public static Vector3Flt operator *(AffineFltMatrix ALeft, Vector3Flt ARight)
        {
            return AffineFltMatrix.Project(ALeft, ARight);
        }
        #endregion
    }
}
