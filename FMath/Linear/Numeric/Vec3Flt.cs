using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Mutable;

namespace FMath.Linear.Numeric
{
    public sealed class Vec3Flt :
        DenseVector<float>,
        IEquatable<Vec3Flt>,
        IAssignable<Vec3Flt>
    {
        #region Static factories
        public static Vec3Flt Zero { get { return new Vec3Flt(0.0f, 0.0f, 0.0f); } }
        public static Vec3Flt One { get { return new Vec3Flt(1.0f, 1.0f, 1.0f); } }
        #endregion

        #region Pure static operators
        [Pure]
        public static Vec3Flt Negate(Vec3Flt ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vec3Flt Add(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vec3Flt Subtract(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vec3Flt Scale(Vec3Flt ALeft, float ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vec3Flt Divide(Vec3Flt ALeft, float ARight)
        {
            return ALeft.Clone().Divide(ARight);
        }
        [Pure]
        public static Vec3Flt Mask(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }
        [Pure]
        public static Vec3Flt Normalize(Vec3Flt ALeft)
        {
            return ALeft.Clone().Normalize();
        }
        [Pure]
        public static float ScalarProduct(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return ALeft.FElements[0] * ARight.FElements[0]
                   + ALeft.FElements[1] * ARight.FElements[1]
                   + ALeft.FElements[2] * ARight.FElements[2];
        }
        [Pure]
        public static Vec3Flt VectorProduct(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return new Vec3Flt(
                ALeft.FElements[1] * ARight.FElements[2] - ALeft.FElements[2] * ARight.FElements[1],
                ALeft.FElements[2] * ARight.FElements[0] - ALeft.FElements[0] * ARight.FElements[2],
                ALeft.FElements[0] * ARight.FElements[1] - ALeft.FElements[1] * ARight.FElements[0]);
        }
        #endregion
        public Vec3Flt()
            : base(3)
        { }
        public Vec3Flt(float AX, float AY, float AZ)
            : base(new[] { AX, AY, AZ }, false)
        { }
        [Pure]
        public new Vec3Flt Clone()
        {
            return new Vec3Flt(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

        #region Mutating chainable operators
        public Vec3Flt Negate()
        {
            this.FElements[0] = -this.FElements[0];
            this.FElements[1] = -this.FElements[1];
            this.FElements[2] = -this.FElements[2];
            return this;
        }
        public Vec3Flt Add(Vec3Flt ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

            this.FElements[0] += ARight.FElements[0];
            this.FElements[1] += ARight.FElements[1];
            this.FElements[2] += ARight.FElements[2];
            return this;
        }
        public Vec3Flt Scale(float ARight)
        {
            this.FElements[0] *= ARight;
            this.FElements[1] *= ARight;
            this.FElements[2] *= ARight;
            return this;
        }
        public Vec3Flt Divide(float ARight)
        {
            this.FElements[0] /= ARight;
            this.FElements[1] /= ARight;
            this.FElements[2] /= ARight;
            return this;
        }
        public Vec3Flt Mask(Vec3Flt ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

            this.FElements[0] *= ARight.FElements[0];
            this.FElements[1] *= ARight.FElements[1];
            this.FElements[2] *= ARight.FElements[2];
            return this;
        }
        public Vec3Flt Normalize()
        {
            this.Divide(this.Length());
            return this;
        }
        #endregion

        #region Pure operators
        [Pure]
        public float Sum()
        {
            return this.FElements[0] + this.FElements[1] + this.FElements[2];
        }
        [Pure]
        public float Product()
        {
            return this.FElements[0] * this.FElements[1] * this.FElements[2];
        }
        [Pure]
        public float Length()
        {
            return (float)Math.Sqrt(Vec3Flt.ScalarProduct(this, this));
        }
        #endregion

        #region IAssignable<Vec3Flt>
        public void Assign(Vec3Flt AFrom)
        {
            if (AFrom == null)
                throw new ArgumentNullException("AFrom");

            this.FElements[0] = AFrom.FElements[0];
            this.FElements[1] = AFrom.FElements[1];
            this.FElements[2] = AFrom.FElements[2];
        }
        #endregion

        #region IEquatable<Vec3Flt>
        [Pure]
        public bool Equals(Vec3Flt AOther)
        {
            if (AOther == null)
                return false;

            return this.FElements[0] == AOther.FElements[0]
                   && this.FElements[1] == AOther.FElements[1]
                   && this.FElements[2] == AOther.FElements[2];
        }
        #endregion

        #region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther is Vec3Flt)
                return this.Equals((Vec3Flt)AOther);

            return base.Equals(AOther);
        }
        #endregion

        public float X
        {
            [Pure]
            get { return this.FElements[0]; }
            set { this.FElements[0] = value; }
        }
        public float Y
        {
            [Pure]
            get { return this.FElements[1]; }
            set { this.FElements[1] = value; }
        }
        public float Z
        {
            [Pure]
            get { return this.FElements[2]; }
            set { this.FElements[2] = value; }
        }

        #region Static operator overloads
        public static bool operator ==(Vec3Flt ALeft, Vec3Flt ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vec3Flt ALeft, Vec3Flt ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vec3Flt operator -(Vec3Flt ALeft)
        {
            return Vec3Flt.Negate(ALeft);
        }
        public static Vec3Flt operator +(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return Vec3Flt.Add(ALeft, ARight);
        }
        public static Vec3Flt operator -(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return Vec3Flt.Subtract(ALeft, ARight);
        }
        public static Vec3Flt operator *(Vec3Flt ALeft, float ARight)
        {
            return Vec3Flt.Scale(ALeft, ARight);
        }
        public static Vec3Flt operator /(Vec3Flt ALeft, float ARight)
        {
            return Vec3Flt.Divide(ALeft, ARight);
        }
        #endregion
    }
}
