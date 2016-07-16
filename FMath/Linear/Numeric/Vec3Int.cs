using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Mutable;

namespace FMath.Linear.Numeric
{
    /// <summary>
    /// Optimized vector class for integer vector of size 3.
    /// </summary>
    /// <seealso cref="System.Int32" />
    /// <seealso cref="Vec3Int" />
    /// <seealso cref="Vec3Int" />
    public class Vec3Int :
        DenseVector<int>,
        IEquatable<Vec3Int>,
        IAssignable<Vec3Int>
    {
        #region Static factories
        /// <summary>
        /// Gets a new zero vector.
        /// </summary>
        /// <value>
        /// A zero vector.
        /// </value>
        public static Vec3Int Zero { get { return new Vec3Int(0, 0, 0); } }
        /// <summary>
        /// Gets a new one vector.
        /// </summary>
        /// <value>
        /// A one vector.
        /// </value>
        public static Vec3Int One { get { return new Vec3Int(1, 1, 1); } }
        #endregion

        #region Pure static operators
        /// <summary>
        /// Gets a negated copy of the specified vector.
        /// </summary>
        /// <param name="ALeft">A vector.</param>
        /// <returns>A negated copy of the vector.</returns>
        [Pure]
        public static Vec3Int Negate(Vec3Int ALeft)
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
        public static Vec3Int Add(Vec3Int ALeft, Vec3Int ARight)
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
        public static Vec3Int Subtract(Vec3Int ALeft, Vec3Int ARight)
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
        public static Vec3Int Scale(Vec3Int ALeft, int ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        /// <summary>
        /// Masks the specified vector.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <returns>The masked vector as a new vector.</returns>
        [Pure]
        public static Vec3Int Mask(Vec3Int ALeft, Vec3Int ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        /// <summary>
        /// Gets the scalar product of two vector.s
        /// </summary>
        /// <param name="ALeft">The left vector.</param>
        /// <param name="ARight">The right vector.</param>
        /// <returns>The scalar product of the two vectors.</returns>
        [Pure]
        public static int ScalarProduct(Vec3Int ALeft, Vec3Int ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
                   + ALeft.FElements[1]*ARight.FElements[1]
                   + ALeft.FElements[2]*ARight.FElements[2];
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Vec3Int"/> class.
        /// </summary>
        public Vec3Int()
            : base(3)
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vec3Int"/> class.
        /// </summary>
        /// <param name="AX">The X value.</param>
        /// <param name="AY">The Y value.</param>
        /// <param name="AZ">The Z value.</param>
        public Vec3Int(int AX, int AY, int AZ)
            : base(new []{AX, AY, AZ}, false)
        { }

        /// <summary>
        /// Clones this vector.
        /// </summary>
        /// <returns>A copy of this vector.</returns>
        [Pure]
        public new Vec3Int Clone()
        {
            return new Vec3Int(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

        #region Mutating chainable operators
        /// <summary>
        /// Negates this vector.
        /// </summary>
        /// <returns>A reference to this vector.</returns>
        public Vec3Int Negate()
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
        public Vec3Int Add(Vec3Int ARight)
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
        public Vec3Int Scale(int ARight)
        {
            this.FElements[0] *= ARight;
            this.FElements[1] *= ARight;
            this.FElements[2] *= ARight;
            return this;
        }
        /// <summary>
        /// Masks this vector.
        /// </summary>
        /// <param name="ARight">The mask.</param>
        /// <returns>A reference to this vector.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when ARight is null.</exception>
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
        /// <summary>
        /// Computes the sum of all elements.
        /// </summary>
        /// <returns>The sum of all elements.</returns>
        [Pure]
        public int Sum()
        {
            return this.FElements[0] + this.FElements[1] + this.FElements[2];
        }
        /// <summary>
        /// Computes the product of all elements.
        /// </summary>
        /// <returns>The product of all elements.</returns>
        [Pure]
        public int Product()
        {
            return this.FElements[0]*this.FElements[1]*this.FElements[2];
        }
        #endregion

        #region IAssignable<Vec3Int>
        /// <inheritDoc />
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
        /// <inheritDoc />
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
        /// <inheritDoc />
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
