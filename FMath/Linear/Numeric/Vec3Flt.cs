using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Mutable;

namespace FMath.Linear.Numeric
{
    /// <summary>
    /// Optimized vector class for float vector of size 3.
    /// </summary>
    /// <seealso cref="System.Int32" />
    /// <seealso cref="Vec3Flt" />
    /// <seealso cref="Vec3Flt" />
    public sealed class Vec3Flt :
        DenseVector<float>,
        IEquatable<Vec3Flt>,
        IAssignable<Vec3Flt>
    {
        #region Static factories
        /// <summary>
        /// Gets a new zero vector.
        /// </summary>
        /// <value>
        /// A zero vector.
        /// </value>
        public static Vec3Flt Zero { get { return new Vec3Flt(0.0f, 0.0f, 0.0f); } }
        /// <summary>
        /// Gets a new one vector.
        /// </summary>
        /// <value>
        /// A one vector.
        /// </value>
        public static Vec3Flt One { get { return new Vec3Flt(1.0f, 1.0f, 1.0f); } }
        #endregion

        #region Pure static operators
        /// <summary>
        /// Gets a negated copy of the specified vector.
        /// </summary>
        /// <param name="ALeft">A vector.</param>
        /// <returns>A negated copy of the vector.</returns>
        [Pure]
        public static Vec3Flt Negate(Vec3Flt ALeft)
        {
            return ALeft.Clone().Negate();
        }
        /// <summary>
        /// Gets the sum of two vectors.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <returns>The sum of the two vectors as a new vector.</returns>
        [Pure]
        public static Vec3Flt Add(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        /// <summary>
        /// Gets the difference of two vectors.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <returns>The difference of the two vectors as a new vector.</returns>
        [Pure]
        public static Vec3Flt Subtract(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        /// <summary>
        /// Gets a scaled version of a vector.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <returns>The scaled vector as a new vector.</returns>
        [Pure]
        public static Vec3Flt Scale(Vec3Flt ALeft, float ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <returns>The divided vector as a new vector.</returns>
        [Pure]
        public static Vec3Flt Divide(Vec3Flt ALeft, float ARight)
        {
            return ALeft.Clone().Divide(ARight);
        }
        /// <summary>
        /// Masks the specified vector.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <returns>The masked vector as a new vector.</returns>
        [Pure]
        public static Vec3Flt Mask(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        /// <summary>
        /// Gets a normalized copy of the specified vector.
        /// </summary>
        /// <param name="ALeft">A vector.</param>
        /// <returns>A normalized copy of the vector.</returns>
        [Pure]
        public static Vec3Flt Normalize(Vec3Flt ALeft)
        {
            return ALeft.Clone().Normalize();
        }

        /// <summary>
        /// Gets the scalar product of two vectors.
        /// </summary>
        /// <param name="ALeft">The left vector.</param>
        /// <param name="ARight">The right vector.</param>
        /// <returns>The scalar product of the two vectors.</returns>
        [Pure]
        public static float ScalarProduct(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return ALeft.FElements[0] * ARight.FElements[0]
                   + ALeft.FElements[1] * ARight.FElements[1]
                   + ALeft.FElements[2] * ARight.FElements[2];
        }
        /// <summary>
        /// Gets the vector product of two vectors.
        /// </summary>
        /// <param name="ALeft">The left vector.</param>
        /// <param name="ARight">The right vector.</param>
        /// <returns>The vector product of the two vectors as a new vector.</returns>
        [Pure]
        public static Vec3Flt VectorProduct(Vec3Flt ALeft, Vec3Flt ARight)
        {
            return new Vec3Flt(
                ALeft.FElements[1] * ARight.FElements[2] - ALeft.FElements[2] * ARight.FElements[1],
                ALeft.FElements[2] * ARight.FElements[0] - ALeft.FElements[0] * ARight.FElements[2],
                ALeft.FElements[0] * ARight.FElements[1] - ALeft.FElements[1] * ARight.FElements[0]);
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Vec3Flt"/> class.
        /// </summary>
        public Vec3Flt()
            : base(3)
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vec3Flt"/> class.
        /// </summary>
        /// <param name="AX">The X value.</param>
        /// <param name="AY">The Y value.</param>
        /// <param name="AZ">The Z value.</param>
        public Vec3Flt(float AX, float AY, float AZ)
            : base(new[] { AX, AY, AZ }, false)
        { }

        /// <summary>
        /// Clones this vector.
        /// </summary>
        /// <returns>A copy of this vector.</returns>
        [Pure]
        public new Vec3Flt Clone()
        {
            return new Vec3Flt(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

        #region Mutating chainable operators
        /// <summary>
        /// Negates this vector.
        /// </summary>
        /// <returns>A reference to this vector.</returns>
        public Vec3Flt Negate()
        {
            this.FElements[0] = -this.FElements[0];
            this.FElements[1] = -this.FElements[1];
            this.FElements[2] = -this.FElements[2];
            return this;
        }
        /// <summary>
        /// Adds the specified vector to this one.
        /// </summary>
        /// <param name="ARight">The other vector.</param>
        /// <returns>A reference to this vector.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when ARight is null.</exception>
        public Vec3Flt Add(Vec3Flt ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

            this.FElements[0] += ARight.FElements[0];
            this.FElements[1] += ARight.FElements[1];
            this.FElements[2] += ARight.FElements[2];
            return this;
        }
        /// <summary>
        /// Scales this vector.
        /// </summary>
        /// <param name="ARight">The scalar.</param>
        /// <returns>A reference to this vector.</returns>
        public Vec3Flt Scale(float ARight)
        {
            this.FElements[0] *= ARight;
            this.FElements[1] *= ARight;
            this.FElements[2] *= ARight;
            return this;
        }
        /// <summary>
        /// Divides this vector by a scalar.
        /// </summary>
        /// <param name="ARight">The scalar.</param>
        /// <returns>A reference to this vector.</returns>
        public Vec3Flt Divide(float ARight)
        {
            this.FElements[0] /= ARight;
            this.FElements[1] /= ARight;
            this.FElements[2] /= ARight;
            return this;
        }
        /// <summary>
        /// Masks this vector.
        /// </summary>
        /// <param name="ARight">The mask.</param>
        /// <returns>A reference to this vector.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when ARight is null.</exception>
        public Vec3Flt Mask(Vec3Flt ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

            this.FElements[0] *= ARight.FElements[0];
            this.FElements[1] *= ARight.FElements[1];
            this.FElements[2] *= ARight.FElements[2];
            return this;
        }
        /// <summary>
        /// Normalizes this vector.
        /// </summary>
        /// <returns>A reference to this vector.</returns>
        public Vec3Flt Normalize()
        {
            this.Divide(this.Length());
            return this;
        }
        #endregion

        #region Pure operators
        /// <summary>
        /// Computes the sum of all elements.
        /// </summary>
        /// <returns>The sum of all elements.</returns>
        [Pure]
        public float Sum()
        {
            return this.FElements[0] + this.FElements[1] + this.FElements[2];
        }
        /// <summary>
        /// Computes the product of all elements.
        /// </summary>
        /// <returns>The product of all elements.</returns>
        [Pure]
        public float Product()
        {
            return this.FElements[0] * this.FElements[1] * this.FElements[2];
        }
        /// <summary>
        /// Gets the euclidean norm of this vector.
        /// </summary>
        /// <returns>The euclidean norm.</returns>
        [Pure]
        public float Length()
        {
            return (float)Math.Sqrt(Vec3Flt.ScalarProduct(this, this));
        }
        #endregion

        #region IAssignable<Vec3Flt>
        /// <inheritDoc />
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
        /// <inheritDoc />
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
        /// <inheritDoc />
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
