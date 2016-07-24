using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Mutable;

namespace FMath.Linear.Numeric
{
    public sealed class HomogeneousFltMatrix :
        DenseMatrix<float>,
        IEquatable<HomogeneousFltMatrix>
    {
        #region Static factories
        public static HomogeneousFltMatrix Zero { get { return new HomogeneousFltMatrix(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f); } }
        public static HomogeneousFltMatrix Identity { get { return new HomogeneousFltMatrix(1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f); } }

        public static HomogeneousFltMatrix RotateX(float AAngle)
        {
            float fCos = (float)Math.Cos(AAngle);
            float fSin = (float)Math.Sin(AAngle);

            return new HomogeneousFltMatrix(
                1.0f, 0.0f, 0.0f,
                0.0f, fCos, -fSin,
                0.0f, fSin, fCos);
        }
        public static HomogeneousFltMatrix RotateY(float AAngle)
        {
            float fCos = (float)Math.Cos(AAngle);
            float fSin = (float)Math.Sin(AAngle);

            return new HomogeneousFltMatrix(
                fCos, 0.0f, fSin,
                0.0f, 1.0f, 0.0f,
                -fSin, 0.0f, fCos);
        }
        public static HomogeneousFltMatrix RotateZ(float AAngle)
        {
            float fCos = (float)Math.Cos(AAngle);
            float fSin = (float)Math.Sin(AAngle);

            return new HomogeneousFltMatrix(
                fCos, -fSin, 0.0f,
                fSin, fCos, 0.0f,
                0.0f, 0.0f, 1.0f);
        }
        public static HomogeneousFltMatrix RotateAxis(float AAngle, Vector3F AAxis)
        {
            if (AAxis == null)
                throw new ArgumentNullException("AAxis");

            float fCos = (float)Math.Cos(AAngle);
            float fSin = (float)Math.Sin(AAngle);
            float fA = 1 - fCos;
            float f1 = AAxis.X, f2 = AAxis.Y, f3 = AAxis.Z;

            return HomogeneousFltMatrix.OrthogonalProjection(AAxis)
                .Scale(fA)
                .Add(new HomogeneousFltMatrix(
                    fCos, -f3*fSin, f2*fSin,
                    f3*fSin, fCos, -f1*fSin,
                    -f2*fSin, f1*fSin, fCos));
        }

        public static HomogeneousFltMatrix Scale(Vector3F AScale)
        {
            if (AScale == null)
                throw new ArgumentNullException("AScale");

            return new HomogeneousFltMatrix(
                AScale.X, 0.0f, 0.0f,
                0.0f, AScale.Y, 0.0f,
                0.0f, 0.0f, AScale.Z);
        }

        public static HomogeneousFltMatrix OrthogonalProjection(Vector3F ALine)
        {
            if (ALine == null)
                throw new ArgumentNullException("ALine");

            float f1 = ALine.X, f2 = ALine.Y, f3 = ALine.Z;

            return new HomogeneousFltMatrix(
                f1 * f1, f1 * f2, f1 * f3,
                f2 * f1, f2 * f2, f2 * f3,
                f3 * f1, f3 * f2, f3 * f3);
        }
        public static HomogeneousFltMatrix OrthogonalProjection(Vector3F AFirst, Vector3F ASecond)
        {
            return HomogeneousFltMatrix.OrthogonalProjection(AFirst).Add(HomogeneousFltMatrix.OrthogonalProjection(ASecond));
        }

        public static HomogeneousFltMatrix Mirror(Vector3F ALine)
        {
            return HomogeneousFltMatrix.OrthogonalProjection(ALine)
                .Scale(2.0f)
                .Add(HomogeneousFltMatrix.Identity.Negate());
        }
        public static HomogeneousFltMatrix Mirror(Vector3F AFirst, Vector3F ASecond)
        {
            return HomogeneousFltMatrix.OrthogonalProjection(AFirst, ASecond)
                .Scale(2.0f)
                .Add(HomogeneousFltMatrix.Identity.Negate());
        }

        public static HomogeneousFltMatrix Parallelepiped(Vector3F AX, Vector3F AY, Vector3F AZ)
        {
            return new HomogeneousFltMatrix(
                AX.X, AX.Y, AX.Z,
                AY.X, AY.Y, AY.Z,
                AZ.X, AZ.Y, AZ.Z);
        }
        #endregion

        #region Pure static operators
        [Pure]
        public static HomogeneousFltMatrix Negate(HomogeneousFltMatrix ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static HomogeneousFltMatrix Add(HomogeneousFltMatrix ALeft, HomogeneousFltMatrix ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static HomogeneousFltMatrix Subtract(HomogeneousFltMatrix ALeft, HomogeneousFltMatrix ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static HomogeneousFltMatrix Scale(HomogeneousFltMatrix ALeft, float ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static HomogeneousFltMatrix Divide(HomogeneousFltMatrix ALeft, float ARight)
        {
            return ALeft.Clone().Divide(ARight);
        }
        [Pure]
        public static HomogeneousFltMatrix Mask(HomogeneousFltMatrix ALeft, HomogeneousFltMatrix ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }
        [Pure]
        public static float Determinant(HomogeneousFltMatrix AMatrix)
        {
            return AMatrix.FCells[0, 0] * AMatrix.FCells[1, 1] * AMatrix.FCells[2, 2]
                   + AMatrix.FCells[1, 0] * AMatrix.FCells[2, 1] * AMatrix.FCells[0, 2]
                   + AMatrix.FCells[2, 0] * AMatrix.FCells[0, 1] * AMatrix.FCells[1, 2]
                   - AMatrix.FCells[0, 0] * AMatrix.FCells[2, 1] * AMatrix.FCells[1, 2]
                   - AMatrix.FCells[2, 0] * AMatrix.FCells[1, 1] * AMatrix.FCells[0, 2]
                   - AMatrix.FCells[1, 0] * AMatrix.FCells[0, 1] * AMatrix.FCells[2, 2];
        }
        [Pure]
        public static HomogeneousFltMatrix Project(HomogeneousFltMatrix ALeft, HomogeneousFltMatrix ARight)
        {
            HomogeneousFltMatrix hfmMatrix = new HomogeneousFltMatrix();
            for (int I = 0; I < 3; I++)
                for (int K = 0; K < 3; K++)
                    for (int J = 0; J < 3; J++)
                        hfmMatrix.FCells[I, K] += ALeft.FCells[I, J] * ARight.FCells[J, K];

            return hfmMatrix;
        }
        [Pure]
        public static Vector3F Project(HomogeneousFltMatrix ALeft, Vector3F ARight)
        {
            return new Vector3F(
                ALeft.FCells[0, 0] * ARight.X + ALeft.FCells[0, 1] * ARight.Y + ALeft.FCells[0, 2] * ARight.Z,
                ALeft.FCells[1, 0] * ARight.X + ALeft.FCells[1, 1] * ARight.Y + ALeft.FCells[1, 2] * ARight.Z,
                ALeft.FCells[2, 0] * ARight.X + ALeft.FCells[2, 1] * ARight.Y + ALeft.FCells[2, 2] * ARight.Z);
        }
        [Pure]
        public static HomogeneousFltMatrix Invert(HomogeneousFltMatrix ALeft)
        {
            float fDet = HomogeneousFltMatrix.Determinant(ALeft);

            if (fDet == 0.0f)
                throw new InvalidOperationException("Cannot invert singular matrices.");

            HomogeneousFltMatrix hmfInverted = new HomogeneousFltMatrix(
                ALeft.FCells[1, 1]*ALeft.FCells[2, 2] - ALeft.FCells[1, 2]*ALeft.FCells[2, 1],
                ALeft.FCells[0, 2]*ALeft.FCells[2, 1] - ALeft.FCells[0, 1]*ALeft.FCells[2, 2],
                ALeft.FCells[0, 1]*ALeft.FCells[1, 2] - ALeft.FCells[0, 2]*ALeft.FCells[1, 1],

                ALeft.FCells[1, 2]*ALeft.FCells[2, 0] - ALeft.FCells[1, 0]*ALeft.FCells[2, 2],
                ALeft.FCells[0, 0]*ALeft.FCells[2, 2] - ALeft.FCells[0, 2]*ALeft.FCells[2, 0],
                ALeft.FCells[0, 2]*ALeft.FCells[1, 0] - ALeft.FCells[0, 0]*ALeft.FCells[1, 2],

                ALeft.FCells[1, 0]*ALeft.FCells[2, 1] - ALeft.FCells[1, 1]*ALeft.FCells[2, 0],
                ALeft.FCells[0, 1]*ALeft.FCells[2, 0] - ALeft.FCells[0, 0]*ALeft.FCells[2, 1],
                ALeft.FCells[0, 0]*ALeft.FCells[1, 1] - ALeft.FCells[0, 1]*ALeft.FCells[1, 0]);

            return hmfInverted.Divide(fDet);
        }
        #endregion

        public HomogeneousFltMatrix()
            : base(new MatrixIndices(3, 3))
        { }
        public HomogeneousFltMatrix(
            float A11, float A12, float A13,
            float A21, float A22, float A23,
            float A31, float A32, float A33)
            : base(new [,] { { A11, A12, A13 }, {A21, A22, A23}, {A31, A32, A33} }, false)
        { }
        
        [Pure]
        public new HomogeneousFltMatrix Clone()
        {
            return new HomogeneousFltMatrix(
                this.FCells[0, 0], this.FCells[0, 1], this.FCells[0, 2],
                this.FCells[1, 0], this.FCells[1, 1], this.FCells[1, 2],
                this.FCells[2, 0], this.FCells[2, 1], this.FCells[2, 2]);
        }

        #region Mutating chainable operators
        public HomogeneousFltMatrix Negate()
        {
            for (int I = 0; I < 9; I++)
                this.FCells[I / 3, I % 3] = -this.FCells[I / 3, I % 3];

            return this;
        }
        public HomogeneousFltMatrix Add(HomogeneousFltMatrix ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

            for (int I = 0; I < 9; I++)
                this.FCells[I / 3, I % 3] += ARight.FCells[I / 3, I % 3];

            return this;
        }
        public HomogeneousFltMatrix Scale(float ARight)
        {
            for (int I = 0; I < 9; I++)
                this.FCells[I / 3, I % 3] *= ARight;

            return this;
        }
        public HomogeneousFltMatrix Divide(float ARight)
        {
            for (int I = 0; I < 9; I++)
                this.FCells[I / 3, I % 3] /= ARight;

            return this;
        }
        public HomogeneousFltMatrix Mask(HomogeneousFltMatrix ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

            for (int I = 0; I < 9; I++)
                this.FCells[I / 3, I % 3] *= ARight.FCells[I / 3, I % 3];

            return this;
        }
        #endregion

        #region IEquatable<HomogeneousFltMatrix>
        [Pure]
        public bool Equals(HomogeneousFltMatrix AOther)
        {
            if (AOther == null)
                return false;

            return AOther.FCells[0, 0] == this.FCells[0, 0]
                   && AOther.FCells[0, 1] == this.FCells[0, 1]
                   && AOther.FCells[0, 2] == this.FCells[0, 2]
                   && AOther.FCells[1, 0] == this.FCells[1, 0]
                   && AOther.FCells[1, 1] == this.FCells[1, 1]
                   && AOther.FCells[1, 2] == this.FCells[1, 2]
                   && AOther.FCells[2, 0] == this.FCells[2, 0]
                   && AOther.FCells[2, 1] == this.FCells[2, 1]
                   && AOther.FCells[2, 2] == this.FCells[2, 2];
        }
        #endregion

        #region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther is HomogeneousFltMatrix)
                return this.Equals((HomogeneousFltMatrix)AOther);

            return base.Equals(AOther);
        }
        #endregion

        #region Static operator overloads
        public static bool operator ==(HomogeneousFltMatrix ALeft, HomogeneousFltMatrix ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(HomogeneousFltMatrix ALeft, HomogeneousFltMatrix ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static HomogeneousFltMatrix operator -(HomogeneousFltMatrix ALeft)
        {
            return HomogeneousFltMatrix.Negate(ALeft);
        }
        public static HomogeneousFltMatrix operator +(HomogeneousFltMatrix ALeft, HomogeneousFltMatrix ARight)
        {
            return HomogeneousFltMatrix.Add(ALeft, ARight);
        }
        public static HomogeneousFltMatrix operator -(HomogeneousFltMatrix ALeft, HomogeneousFltMatrix ARight)
        {
            return HomogeneousFltMatrix.Subtract(ALeft, ARight);
        }
        public static HomogeneousFltMatrix operator *(HomogeneousFltMatrix ALeft, float ARight)
        {
            return HomogeneousFltMatrix.Scale(ALeft, ARight);
        }
        public static HomogeneousFltMatrix operator /(HomogeneousFltMatrix ALeft, float ARight)
        {
            return HomogeneousFltMatrix.Divide(ALeft, ARight);
        }

        public static HomogeneousFltMatrix operator *(HomogeneousFltMatrix ALeft, HomogeneousFltMatrix ARight)
        {
            return HomogeneousFltMatrix.Project(ALeft, ARight);
        }
        public static Vector3F operator *(HomogeneousFltMatrix ALeft, Vector3F ARight)
        {
            return HomogeneousFltMatrix.Project(ALeft, ARight);
        }
        #endregion
    }
}
