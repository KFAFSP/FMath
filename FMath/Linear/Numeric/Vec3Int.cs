using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Mutable;

namespace FMath.Linear.Numeric
{
    public sealed class Vec3Int :
        DenseVector<int>,
        IEquatable<Vec3Int>,
        IAssignable<Vec3Int>
    {
        #region Static factories
        public static Vec3Int Zero { get { return new Vec3Int(0, 0, 0); } }
        public static Vec3Int One { get { return new Vec3Int(1, 1, 1); } }
        #endregion

        #region Pure static operators
        [Pure]
        public static Vec3Int Negate(Vec3Int ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vec3Int Add(Vec3Int ALeft, Vec3Int ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vec3Int Subtract(Vec3Int ALeft, Vec3Int ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vec3Int Scale(Vec3Int ALeft, int ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vec3Int Mask(Vec3Int ALeft, Vec3Int ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }
        [Pure]
        public static int ScalarProduct(Vec3Int ALeft, Vec3Int ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
                   + ALeft.FElements[1]*ARight.FElements[1]
                   + ALeft.FElements[2]*ARight.FElements[2];
        }
        [Pure]
        public static Vec3Int VectorProduct(Vec3Int ALeft, Vec3Int ARight)
        {
            return new Vec3Int(
                ALeft.FElements[1]*ARight.FElements[2]-ALeft.FElements[2]*ARight.FElements[1],
                ALeft.FElements[2]*ARight.FElements[0]-ALeft.FElements[0]*ARight.FElements[2],
                ALeft.FElements[0]*ARight.FElements[1]-ALeft.FElements[1]*ARight.FElements[0]);
        }
        #endregion
        public Vec3Int()
            : base(3)
        { }
        public Vec3Int(int AX, int AY, int AZ)
            : base(new []{AX, AY, AZ}, false)
        { }
        [Pure]
        public new Vec3Int Clone()
        {
            return new Vec3Int(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

        #region Mutating chainable operators
        public Vec3Int Negate()
        {
            this.FElements[0] = -this.FElements[0];
            this.FElements[1] = -this.FElements[1];
            this.FElements[2] = -this.FElements[2];
            return this;
        }
        public Vec3Int Add(Vec3Int ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

            this.FElements[0] += ARight.FElements[0];
            this.FElements[1] += ARight.FElements[1];
            this.FElements[2] += ARight.FElements[2];
            return this;
        }
        public Vec3Int Scale(int ARight)
        {
            this.FElements[0] *= ARight;
            this.FElements[1] *= ARight;
            this.FElements[2] *= ARight;
            return this;
        }
        public Vec3Int Mask(Vec3Int ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

            this.FElements[0] *= ARight.FElements[0];
            this.FElements[1] *= ARight.FElements[1];
            this.FElements[2] *= ARight.FElements[2];
            return this;
        }
        #endregion

        #region Pure operators
        [Pure]
        public int Sum()
        {
            return this.FElements[0] + this.FElements[1] + this.FElements[2];
        }
        [Pure]
        public int Product()
        {
            return this.FElements[0]*this.FElements[1]*this.FElements[2];
        }
        #endregion

        #region IAssignable<Vec3Int>
        public void Assign(Vec3Int AFrom)
        {
            if (AFrom == null)
                throw new ArgumentNullException("AFrom");

            this.FElements[0] = AFrom.FElements[0];
            this.FElements[1] = AFrom.FElements[1];
            this.FElements[2] = AFrom.FElements[2];
        }
        #endregion

        #region IEquatable<Vec3Int>
        [Pure]
        public bool Equals(Vec3Int AOther)
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
            if (AOther is Vec3Int)
                return this.Equals((Vec3Int)AOther);

            return base.Equals(AOther);
        }
        #endregion

        public int X
        {
            [Pure]
            get { return this.FElements[0]; }
            set { this.FElements[0] = value; }
        }
        public int Y
        {
            [Pure]
            get { return this.FElements[1]; }
            set { this.FElements[1] = value; }
        }
        public int Z
        {
            [Pure]
            get { return this.FElements[2]; }
            set { this.FElements[2] = value; }
        }

        #region Static operator overloads
        public static bool operator ==(Vec3Int ALeft, Vec3Int ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vec3Int ALeft, Vec3Int ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vec3Int operator -(Vec3Int ALeft)
        {
            return Vec3Int.Negate(ALeft);
        }
        public static Vec3Int operator +(Vec3Int ALeft, Vec3Int ARight)
        {
            return Vec3Int.Add(ALeft, ARight);
        }
        public static Vec3Int operator -(Vec3Int ALeft, Vec3Int ARight)
        {
            return Vec3Int.Subtract(ALeft, ARight);
        }
        public static Vec3Int operator *(Vec3Int ALeft, int ARight)
        {
            return Vec3Int.Scale(ALeft, ARight);
        }
        #endregion
    }
}
