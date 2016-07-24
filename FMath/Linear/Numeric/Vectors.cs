using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Mutable;

namespace FMath.Linear.Numeric
{
    public sealed class Vector2I :
		DenseVector<int>,
		IEquatable<Vector2I>,
		IAssignable<Vector2I>
	{
		#region Static factories
        public static Vector2I Zero { get { return new Vector2I(0, 0); } }
        public static Vector2I One { get { return new Vector2I(1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector2I Negate(Vector2I ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector2I Add(Vector2I ALeft, Vector2I ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector2I Subtract(Vector2I ALeft, Vector2I ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector2I Scale(Vector2I ALeft, int ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector2I Mask(Vector2I ALeft, Vector2I ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static int ScalarProduct(Vector2I ALeft, Vector2I ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1];
        }
        #endregion

        public Vector2I()
			: base(2)
		{ }
		public Vector2I(int AX, int AY)
			: base(new []{AX, AY}, false)
		{ }

		[Pure]
        public new Vector2I Clone()
        {
            return new Vector2I(this.FElements[0], this.FElements[1]);
        }

		#region IEquatable<Vector2I>
		[Pure]
		public bool Equals(Vector2I AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1];
		}
		#endregion

		#region IAssignable<Vector2I>
		public void Assign(Vector2I AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
		}
		#endregion

		#region Mutating chainable operators
        public Vector2I Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
            return this;
        }
        public Vector2I Add(Vector2I ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
            return this;
        }
        public Vector2I Scale(int ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
            return this;
        }
        public Vector2I Mask(Vector2I ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector2I)
                return this.Equals((Vector2I)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public int LengthSq
		{
			get { return Vector2I.ScalarProduct(this, this); }
		}

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

		#region Static operator overloads
        public static bool operator ==(Vector2I ALeft, Vector2I ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector2I ALeft, Vector2I ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector2I operator -(Vector2I ALeft)
        {
            return Vector2I.Negate(ALeft);
        }
        public static Vector2I operator +(Vector2I ALeft, Vector2I ARight)
        {
            return Vector2I.Add(ALeft, ARight);
        }
        public static Vector2I operator -(Vector2I ALeft, Vector2I ARight)
        {
            return Vector2I.Subtract(ALeft, ARight);
        }
        public static Vector2I operator *(Vector2I ALeft, int ARight)
        {
            return Vector2I.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector2F :
		DenseVector<float>,
		IEquatable<Vector2F>,
		IAssignable<Vector2F>
	{
		#region Static factories
        public static Vector2F Zero { get { return new Vector2F(0, 0); } }
        public static Vector2F One { get { return new Vector2F(1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector2F Negate(Vector2F ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector2F Add(Vector2F ALeft, Vector2F ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector2F Subtract(Vector2F ALeft, Vector2F ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector2F Scale(Vector2F ALeft, float ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector2F Mask(Vector2F ALeft, Vector2F ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static float ScalarProduct(Vector2F ALeft, Vector2F ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1];
        }
        #endregion

        public Vector2F()
			: base(2)
		{ }
		public Vector2F(float AX, float AY)
			: base(new []{AX, AY}, false)
		{ }

		[Pure]
        public new Vector2F Clone()
        {
            return new Vector2F(this.FElements[0], this.FElements[1]);
        }

		#region IEquatable<Vector2F>
		[Pure]
		public bool Equals(Vector2F AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1];
		}
		#endregion

		#region IAssignable<Vector2F>
		public void Assign(Vector2F AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
		}
		#endregion

		#region Mutating chainable operators
        public Vector2F Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
            return this;
        }
        public Vector2F Add(Vector2F ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
            return this;
        }
        public Vector2F Scale(float ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
            return this;
        }
        public Vector2F Mask(Vector2F ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector2F)
                return this.Equals((Vector2F)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public float LengthSq
		{
			get { return Vector2F.ScalarProduct(this, this); }
		}

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

		#region Static operator overloads
        public static bool operator ==(Vector2F ALeft, Vector2F ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector2F ALeft, Vector2F ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector2F operator -(Vector2F ALeft)
        {
            return Vector2F.Negate(ALeft);
        }
        public static Vector2F operator +(Vector2F ALeft, Vector2F ARight)
        {
            return Vector2F.Add(ALeft, ARight);
        }
        public static Vector2F operator -(Vector2F ALeft, Vector2F ARight)
        {
            return Vector2F.Subtract(ALeft, ARight);
        }
        public static Vector2F operator *(Vector2F ALeft, float ARight)
        {
            return Vector2F.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector2D :
		DenseVector<double>,
		IEquatable<Vector2D>,
		IAssignable<Vector2D>
	{
		#region Static factories
        public static Vector2D Zero { get { return new Vector2D(0, 0); } }
        public static Vector2D One { get { return new Vector2D(1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector2D Negate(Vector2D ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector2D Add(Vector2D ALeft, Vector2D ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector2D Subtract(Vector2D ALeft, Vector2D ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector2D Scale(Vector2D ALeft, double ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector2D Mask(Vector2D ALeft, Vector2D ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static double ScalarProduct(Vector2D ALeft, Vector2D ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1];
        }
        #endregion

        public Vector2D()
			: base(2)
		{ }
		public Vector2D(double AX, double AY)
			: base(new []{AX, AY}, false)
		{ }

		[Pure]
        public new Vector2D Clone()
        {
            return new Vector2D(this.FElements[0], this.FElements[1]);
        }

		#region IEquatable<Vector2D>
		[Pure]
		public bool Equals(Vector2D AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1];
		}
		#endregion

		#region IAssignable<Vector2D>
		public void Assign(Vector2D AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
		}
		#endregion

		#region Mutating chainable operators
        public Vector2D Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
            return this;
        }
        public Vector2D Add(Vector2D ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
            return this;
        }
        public Vector2D Scale(double ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
            return this;
        }
        public Vector2D Mask(Vector2D ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector2D)
                return this.Equals((Vector2D)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public double LengthSq
		{
			get { return Vector2D.ScalarProduct(this, this); }
		}

		public double X
		{
			[Pure]
			get { return this.FElements[0]; }
			set { this.FElements[0] = value; }
		}
		public double Y
		{
			[Pure]
			get { return this.FElements[1]; }
			set { this.FElements[1] = value; }
		}

		#region Static operator overloads
        public static bool operator ==(Vector2D ALeft, Vector2D ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector2D ALeft, Vector2D ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector2D operator -(Vector2D ALeft)
        {
            return Vector2D.Negate(ALeft);
        }
        public static Vector2D operator +(Vector2D ALeft, Vector2D ARight)
        {
            return Vector2D.Add(ALeft, ARight);
        }
        public static Vector2D operator -(Vector2D ALeft, Vector2D ARight)
        {
            return Vector2D.Subtract(ALeft, ARight);
        }
        public static Vector2D operator *(Vector2D ALeft, double ARight)
        {
            return Vector2D.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector2M :
		DenseVector<decimal>,
		IEquatable<Vector2M>,
		IAssignable<Vector2M>
	{
		#region Static factories
        public static Vector2M Zero { get { return new Vector2M(0, 0); } }
        public static Vector2M One { get { return new Vector2M(1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector2M Negate(Vector2M ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector2M Add(Vector2M ALeft, Vector2M ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector2M Subtract(Vector2M ALeft, Vector2M ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector2M Scale(Vector2M ALeft, decimal ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector2M Mask(Vector2M ALeft, Vector2M ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static decimal ScalarProduct(Vector2M ALeft, Vector2M ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1];
        }
        #endregion

        public Vector2M()
			: base(2)
		{ }
		public Vector2M(decimal AX, decimal AY)
			: base(new []{AX, AY}, false)
		{ }

		[Pure]
        public new Vector2M Clone()
        {
            return new Vector2M(this.FElements[0], this.FElements[1]);
        }

		#region IEquatable<Vector2M>
		[Pure]
		public bool Equals(Vector2M AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1];
		}
		#endregion

		#region IAssignable<Vector2M>
		public void Assign(Vector2M AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
		}
		#endregion

		#region Mutating chainable operators
        public Vector2M Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
            return this;
        }
        public Vector2M Add(Vector2M ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
            return this;
        }
        public Vector2M Scale(decimal ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
            return this;
        }
        public Vector2M Mask(Vector2M ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector2M)
                return this.Equals((Vector2M)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public decimal LengthSq
		{
			get { return Vector2M.ScalarProduct(this, this); }
		}

		public decimal X
		{
			[Pure]
			get { return this.FElements[0]; }
			set { this.FElements[0] = value; }
		}
		public decimal Y
		{
			[Pure]
			get { return this.FElements[1]; }
			set { this.FElements[1] = value; }
		}

		#region Static operator overloads
        public static bool operator ==(Vector2M ALeft, Vector2M ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector2M ALeft, Vector2M ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector2M operator -(Vector2M ALeft)
        {
            return Vector2M.Negate(ALeft);
        }
        public static Vector2M operator +(Vector2M ALeft, Vector2M ARight)
        {
            return Vector2M.Add(ALeft, ARight);
        }
        public static Vector2M operator -(Vector2M ALeft, Vector2M ARight)
        {
            return Vector2M.Subtract(ALeft, ARight);
        }
        public static Vector2M operator *(Vector2M ALeft, decimal ARight)
        {
            return Vector2M.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector3I :
		DenseVector<int>,
		IEquatable<Vector3I>,
		IAssignable<Vector3I>
	{
		#region Static factories
        public static Vector3I Zero { get { return new Vector3I(0, 0, 0); } }
        public static Vector3I One { get { return new Vector3I(1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector3I Negate(Vector3I ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector3I Add(Vector3I ALeft, Vector3I ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector3I Subtract(Vector3I ALeft, Vector3I ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector3I Scale(Vector3I ALeft, int ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector3I Mask(Vector3I ALeft, Vector3I ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static int ScalarProduct(Vector3I ALeft, Vector3I ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2];
        }
		[Pure]
        public static Vector3I VectorProduct(Vector3I ALeft, Vector3I ARight)
        {
            return new Vector3I(
                ALeft.FElements[1] * ARight.FElements[2] - ALeft.FElements[2] * ARight.FElements[1],
                ALeft.FElements[2] * ARight.FElements[0] - ALeft.FElements[0] * ARight.FElements[2],
                ALeft.FElements[0] * ARight.FElements[1] - ALeft.FElements[1] * ARight.FElements[0]);
        }
        #endregion

        public Vector3I()
			: base(3)
		{ }
		public Vector3I(int AX, int AY, int AZ)
			: base(new []{AX, AY, AZ}, false)
		{ }

		[Pure]
        public new Vector3I Clone()
        {
            return new Vector3I(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

		#region IEquatable<Vector3I>
		[Pure]
		public bool Equals(Vector3I AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2];
		}
		#endregion

		#region IAssignable<Vector3I>
		public void Assign(Vector3I AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
		}
		#endregion

		#region Mutating chainable operators
        public Vector3I Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
            return this;
        }
        public Vector3I Add(Vector3I ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
            return this;
        }
        public Vector3I Scale(int ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
            return this;
        }
        public Vector3I Mask(Vector3I ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
			this.FElements[2] *= ARight.FElements[2];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector3I)
                return this.Equals((Vector3I)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public int LengthSq
		{
			get { return Vector3I.ScalarProduct(this, this); }
		}

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
        public static bool operator ==(Vector3I ALeft, Vector3I ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector3I ALeft, Vector3I ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector3I operator -(Vector3I ALeft)
        {
            return Vector3I.Negate(ALeft);
        }
        public static Vector3I operator +(Vector3I ALeft, Vector3I ARight)
        {
            return Vector3I.Add(ALeft, ARight);
        }
        public static Vector3I operator -(Vector3I ALeft, Vector3I ARight)
        {
            return Vector3I.Subtract(ALeft, ARight);
        }
        public static Vector3I operator *(Vector3I ALeft, int ARight)
        {
            return Vector3I.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector3F :
		DenseVector<float>,
		IEquatable<Vector3F>,
		IAssignable<Vector3F>
	{
		#region Static factories
        public static Vector3F Zero { get { return new Vector3F(0, 0, 0); } }
        public static Vector3F One { get { return new Vector3F(1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector3F Negate(Vector3F ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector3F Add(Vector3F ALeft, Vector3F ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector3F Subtract(Vector3F ALeft, Vector3F ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector3F Scale(Vector3F ALeft, float ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector3F Mask(Vector3F ALeft, Vector3F ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static float ScalarProduct(Vector3F ALeft, Vector3F ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2];
        }
		[Pure]
        public static Vector3F VectorProduct(Vector3F ALeft, Vector3F ARight)
        {
            return new Vector3F(
                ALeft.FElements[1] * ARight.FElements[2] - ALeft.FElements[2] * ARight.FElements[1],
                ALeft.FElements[2] * ARight.FElements[0] - ALeft.FElements[0] * ARight.FElements[2],
                ALeft.FElements[0] * ARight.FElements[1] - ALeft.FElements[1] * ARight.FElements[0]);
        }
        #endregion

        public Vector3F()
			: base(3)
		{ }
		public Vector3F(float AX, float AY, float AZ)
			: base(new []{AX, AY, AZ}, false)
		{ }

		[Pure]
        public new Vector3F Clone()
        {
            return new Vector3F(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

		#region IEquatable<Vector3F>
		[Pure]
		public bool Equals(Vector3F AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2];
		}
		#endregion

		#region IAssignable<Vector3F>
		public void Assign(Vector3F AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
		}
		#endregion

		#region Mutating chainable operators
        public Vector3F Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
            return this;
        }
        public Vector3F Add(Vector3F ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
            return this;
        }
        public Vector3F Scale(float ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
            return this;
        }
        public Vector3F Mask(Vector3F ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
			this.FElements[2] *= ARight.FElements[2];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector3F)
                return this.Equals((Vector3F)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public float LengthSq
		{
			get { return Vector3F.ScalarProduct(this, this); }
		}

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
        public static bool operator ==(Vector3F ALeft, Vector3F ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector3F ALeft, Vector3F ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector3F operator -(Vector3F ALeft)
        {
            return Vector3F.Negate(ALeft);
        }
        public static Vector3F operator +(Vector3F ALeft, Vector3F ARight)
        {
            return Vector3F.Add(ALeft, ARight);
        }
        public static Vector3F operator -(Vector3F ALeft, Vector3F ARight)
        {
            return Vector3F.Subtract(ALeft, ARight);
        }
        public static Vector3F operator *(Vector3F ALeft, float ARight)
        {
            return Vector3F.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector3D :
		DenseVector<double>,
		IEquatable<Vector3D>,
		IAssignable<Vector3D>
	{
		#region Static factories
        public static Vector3D Zero { get { return new Vector3D(0, 0, 0); } }
        public static Vector3D One { get { return new Vector3D(1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector3D Negate(Vector3D ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector3D Add(Vector3D ALeft, Vector3D ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector3D Subtract(Vector3D ALeft, Vector3D ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector3D Scale(Vector3D ALeft, double ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector3D Mask(Vector3D ALeft, Vector3D ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static double ScalarProduct(Vector3D ALeft, Vector3D ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2];
        }
		[Pure]
        public static Vector3D VectorProduct(Vector3D ALeft, Vector3D ARight)
        {
            return new Vector3D(
                ALeft.FElements[1] * ARight.FElements[2] - ALeft.FElements[2] * ARight.FElements[1],
                ALeft.FElements[2] * ARight.FElements[0] - ALeft.FElements[0] * ARight.FElements[2],
                ALeft.FElements[0] * ARight.FElements[1] - ALeft.FElements[1] * ARight.FElements[0]);
        }
        #endregion

        public Vector3D()
			: base(3)
		{ }
		public Vector3D(double AX, double AY, double AZ)
			: base(new []{AX, AY, AZ}, false)
		{ }

		[Pure]
        public new Vector3D Clone()
        {
            return new Vector3D(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

		#region IEquatable<Vector3D>
		[Pure]
		public bool Equals(Vector3D AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2];
		}
		#endregion

		#region IAssignable<Vector3D>
		public void Assign(Vector3D AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
		}
		#endregion

		#region Mutating chainable operators
        public Vector3D Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
            return this;
        }
        public Vector3D Add(Vector3D ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
            return this;
        }
        public Vector3D Scale(double ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
            return this;
        }
        public Vector3D Mask(Vector3D ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
			this.FElements[2] *= ARight.FElements[2];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector3D)
                return this.Equals((Vector3D)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public double LengthSq
		{
			get { return Vector3D.ScalarProduct(this, this); }
		}

		public double X
		{
			[Pure]
			get { return this.FElements[0]; }
			set { this.FElements[0] = value; }
		}
		public double Y
		{
			[Pure]
			get { return this.FElements[1]; }
			set { this.FElements[1] = value; }
		}
		public double Z
		{
			[Pure]
			get { return this.FElements[2]; }
			set { this.FElements[2] = value; }
		}

		#region Static operator overloads
        public static bool operator ==(Vector3D ALeft, Vector3D ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector3D ALeft, Vector3D ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector3D operator -(Vector3D ALeft)
        {
            return Vector3D.Negate(ALeft);
        }
        public static Vector3D operator +(Vector3D ALeft, Vector3D ARight)
        {
            return Vector3D.Add(ALeft, ARight);
        }
        public static Vector3D operator -(Vector3D ALeft, Vector3D ARight)
        {
            return Vector3D.Subtract(ALeft, ARight);
        }
        public static Vector3D operator *(Vector3D ALeft, double ARight)
        {
            return Vector3D.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector3M :
		DenseVector<decimal>,
		IEquatable<Vector3M>,
		IAssignable<Vector3M>
	{
		#region Static factories
        public static Vector3M Zero { get { return new Vector3M(0, 0, 0); } }
        public static Vector3M One { get { return new Vector3M(1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector3M Negate(Vector3M ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector3M Add(Vector3M ALeft, Vector3M ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector3M Subtract(Vector3M ALeft, Vector3M ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector3M Scale(Vector3M ALeft, decimal ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector3M Mask(Vector3M ALeft, Vector3M ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static decimal ScalarProduct(Vector3M ALeft, Vector3M ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2];
        }
		[Pure]
        public static Vector3M VectorProduct(Vector3M ALeft, Vector3M ARight)
        {
            return new Vector3M(
                ALeft.FElements[1] * ARight.FElements[2] - ALeft.FElements[2] * ARight.FElements[1],
                ALeft.FElements[2] * ARight.FElements[0] - ALeft.FElements[0] * ARight.FElements[2],
                ALeft.FElements[0] * ARight.FElements[1] - ALeft.FElements[1] * ARight.FElements[0]);
        }
        #endregion

        public Vector3M()
			: base(3)
		{ }
		public Vector3M(decimal AX, decimal AY, decimal AZ)
			: base(new []{AX, AY, AZ}, false)
		{ }

		[Pure]
        public new Vector3M Clone()
        {
            return new Vector3M(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

		#region IEquatable<Vector3M>
		[Pure]
		public bool Equals(Vector3M AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2];
		}
		#endregion

		#region IAssignable<Vector3M>
		public void Assign(Vector3M AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
		}
		#endregion

		#region Mutating chainable operators
        public Vector3M Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
            return this;
        }
        public Vector3M Add(Vector3M ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
            return this;
        }
        public Vector3M Scale(decimal ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
            return this;
        }
        public Vector3M Mask(Vector3M ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
			this.FElements[2] *= ARight.FElements[2];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector3M)
                return this.Equals((Vector3M)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public decimal LengthSq
		{
			get { return Vector3M.ScalarProduct(this, this); }
		}

		public decimal X
		{
			[Pure]
			get { return this.FElements[0]; }
			set { this.FElements[0] = value; }
		}
		public decimal Y
		{
			[Pure]
			get { return this.FElements[1]; }
			set { this.FElements[1] = value; }
		}
		public decimal Z
		{
			[Pure]
			get { return this.FElements[2]; }
			set { this.FElements[2] = value; }
		}

		#region Static operator overloads
        public static bool operator ==(Vector3M ALeft, Vector3M ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector3M ALeft, Vector3M ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector3M operator -(Vector3M ALeft)
        {
            return Vector3M.Negate(ALeft);
        }
        public static Vector3M operator +(Vector3M ALeft, Vector3M ARight)
        {
            return Vector3M.Add(ALeft, ARight);
        }
        public static Vector3M operator -(Vector3M ALeft, Vector3M ARight)
        {
            return Vector3M.Subtract(ALeft, ARight);
        }
        public static Vector3M operator *(Vector3M ALeft, decimal ARight)
        {
            return Vector3M.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector4I :
		DenseVector<int>,
		IEquatable<Vector4I>,
		IAssignable<Vector4I>
	{
		#region Static factories
        public static Vector4I Zero { get { return new Vector4I(0, 0, 0, 0); } }
        public static Vector4I One { get { return new Vector4I(1, 1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector4I Negate(Vector4I ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector4I Add(Vector4I ALeft, Vector4I ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector4I Subtract(Vector4I ALeft, Vector4I ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector4I Scale(Vector4I ALeft, int ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector4I Mask(Vector4I ALeft, Vector4I ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static int ScalarProduct(Vector4I ALeft, Vector4I ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2]
				 + ALeft.FElements[3]*ARight.FElements[3];
        }
        #endregion

        public Vector4I()
			: base(4)
		{ }
		public Vector4I(int AX, int AY, int AZ, int AW)
			: base(new []{AX, AY, AZ, AW}, false)
		{ }

		[Pure]
        public new Vector4I Clone()
        {
            return new Vector4I(this.FElements[0], this.FElements[1], this.FElements[2], this.FElements[3]);
        }

		#region IEquatable<Vector4I>
		[Pure]
		public bool Equals(Vector4I AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2]
				 && this.FElements[3] == AOther.FElements[3];
		}
		#endregion

		#region IAssignable<Vector4I>
		public void Assign(Vector4I AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
			this.FElements[3] = AFrom.FElements[3];
		}
		#endregion

		#region Mutating chainable operators
        public Vector4I Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
			this.FElements[3] = -this.FElements[3];
            return this;
        }
        public Vector4I Add(Vector4I ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
			this.FElements[3] += ARight.FElements[3];
            return this;
        }
        public Vector4I Scale(int ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
			this.FElements[3] *= ARight;
            return this;
        }
        public Vector4I Mask(Vector4I ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
			this.FElements[2] *= ARight.FElements[2];
			this.FElements[3] *= ARight.FElements[3];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector4I)
                return this.Equals((Vector4I)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public int LengthSq
		{
			get { return Vector4I.ScalarProduct(this, this); }
		}

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
		public int W
		{
			[Pure]
			get { return this.FElements[3]; }
			set { this.FElements[3] = value; }
		}

		#region Static operator overloads
        public static bool operator ==(Vector4I ALeft, Vector4I ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector4I ALeft, Vector4I ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector4I operator -(Vector4I ALeft)
        {
            return Vector4I.Negate(ALeft);
        }
        public static Vector4I operator +(Vector4I ALeft, Vector4I ARight)
        {
            return Vector4I.Add(ALeft, ARight);
        }
        public static Vector4I operator -(Vector4I ALeft, Vector4I ARight)
        {
            return Vector4I.Subtract(ALeft, ARight);
        }
        public static Vector4I operator *(Vector4I ALeft, int ARight)
        {
            return Vector4I.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector4F :
		DenseVector<float>,
		IEquatable<Vector4F>,
		IAssignable<Vector4F>
	{
		#region Static factories
        public static Vector4F Zero { get { return new Vector4F(0, 0, 0, 0); } }
        public static Vector4F One { get { return new Vector4F(1, 1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector4F Negate(Vector4F ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector4F Add(Vector4F ALeft, Vector4F ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector4F Subtract(Vector4F ALeft, Vector4F ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector4F Scale(Vector4F ALeft, float ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector4F Mask(Vector4F ALeft, Vector4F ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static float ScalarProduct(Vector4F ALeft, Vector4F ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2]
				 + ALeft.FElements[3]*ARight.FElements[3];
        }
        #endregion

        public Vector4F()
			: base(4)
		{ }
		public Vector4F(float AX, float AY, float AZ, float AW)
			: base(new []{AX, AY, AZ, AW}, false)
		{ }

		[Pure]
        public new Vector4F Clone()
        {
            return new Vector4F(this.FElements[0], this.FElements[1], this.FElements[2], this.FElements[3]);
        }

		#region IEquatable<Vector4F>
		[Pure]
		public bool Equals(Vector4F AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2]
				 && this.FElements[3] == AOther.FElements[3];
		}
		#endregion

		#region IAssignable<Vector4F>
		public void Assign(Vector4F AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
			this.FElements[3] = AFrom.FElements[3];
		}
		#endregion

		#region Mutating chainable operators
        public Vector4F Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
			this.FElements[3] = -this.FElements[3];
            return this;
        }
        public Vector4F Add(Vector4F ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
			this.FElements[3] += ARight.FElements[3];
            return this;
        }
        public Vector4F Scale(float ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
			this.FElements[3] *= ARight;
            return this;
        }
        public Vector4F Mask(Vector4F ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
			this.FElements[2] *= ARight.FElements[2];
			this.FElements[3] *= ARight.FElements[3];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector4F)
                return this.Equals((Vector4F)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public float LengthSq
		{
			get { return Vector4F.ScalarProduct(this, this); }
		}

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
		public float W
		{
			[Pure]
			get { return this.FElements[3]; }
			set { this.FElements[3] = value; }
		}

		#region Static operator overloads
        public static bool operator ==(Vector4F ALeft, Vector4F ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector4F ALeft, Vector4F ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector4F operator -(Vector4F ALeft)
        {
            return Vector4F.Negate(ALeft);
        }
        public static Vector4F operator +(Vector4F ALeft, Vector4F ARight)
        {
            return Vector4F.Add(ALeft, ARight);
        }
        public static Vector4F operator -(Vector4F ALeft, Vector4F ARight)
        {
            return Vector4F.Subtract(ALeft, ARight);
        }
        public static Vector4F operator *(Vector4F ALeft, float ARight)
        {
            return Vector4F.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector4D :
		DenseVector<double>,
		IEquatable<Vector4D>,
		IAssignable<Vector4D>
	{
		#region Static factories
        public static Vector4D Zero { get { return new Vector4D(0, 0, 0, 0); } }
        public static Vector4D One { get { return new Vector4D(1, 1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector4D Negate(Vector4D ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector4D Add(Vector4D ALeft, Vector4D ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector4D Subtract(Vector4D ALeft, Vector4D ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector4D Scale(Vector4D ALeft, double ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector4D Mask(Vector4D ALeft, Vector4D ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static double ScalarProduct(Vector4D ALeft, Vector4D ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2]
				 + ALeft.FElements[3]*ARight.FElements[3];
        }
        #endregion

        public Vector4D()
			: base(4)
		{ }
		public Vector4D(double AX, double AY, double AZ, double AW)
			: base(new []{AX, AY, AZ, AW}, false)
		{ }

		[Pure]
        public new Vector4D Clone()
        {
            return new Vector4D(this.FElements[0], this.FElements[1], this.FElements[2], this.FElements[3]);
        }

		#region IEquatable<Vector4D>
		[Pure]
		public bool Equals(Vector4D AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2]
				 && this.FElements[3] == AOther.FElements[3];
		}
		#endregion

		#region IAssignable<Vector4D>
		public void Assign(Vector4D AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
			this.FElements[3] = AFrom.FElements[3];
		}
		#endregion

		#region Mutating chainable operators
        public Vector4D Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
			this.FElements[3] = -this.FElements[3];
            return this;
        }
        public Vector4D Add(Vector4D ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
			this.FElements[3] += ARight.FElements[3];
            return this;
        }
        public Vector4D Scale(double ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
			this.FElements[3] *= ARight;
            return this;
        }
        public Vector4D Mask(Vector4D ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
			this.FElements[2] *= ARight.FElements[2];
			this.FElements[3] *= ARight.FElements[3];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector4D)
                return this.Equals((Vector4D)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public double LengthSq
		{
			get { return Vector4D.ScalarProduct(this, this); }
		}

		public double X
		{
			[Pure]
			get { return this.FElements[0]; }
			set { this.FElements[0] = value; }
		}
		public double Y
		{
			[Pure]
			get { return this.FElements[1]; }
			set { this.FElements[1] = value; }
		}
		public double Z
		{
			[Pure]
			get { return this.FElements[2]; }
			set { this.FElements[2] = value; }
		}
		public double W
		{
			[Pure]
			get { return this.FElements[3]; }
			set { this.FElements[3] = value; }
		}

		#region Static operator overloads
        public static bool operator ==(Vector4D ALeft, Vector4D ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector4D ALeft, Vector4D ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector4D operator -(Vector4D ALeft)
        {
            return Vector4D.Negate(ALeft);
        }
        public static Vector4D operator +(Vector4D ALeft, Vector4D ARight)
        {
            return Vector4D.Add(ALeft, ARight);
        }
        public static Vector4D operator -(Vector4D ALeft, Vector4D ARight)
        {
            return Vector4D.Subtract(ALeft, ARight);
        }
        public static Vector4D operator *(Vector4D ALeft, double ARight)
        {
            return Vector4D.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector4M :
		DenseVector<decimal>,
		IEquatable<Vector4M>,
		IAssignable<Vector4M>
	{
		#region Static factories
        public static Vector4M Zero { get { return new Vector4M(0, 0, 0, 0); } }
        public static Vector4M One { get { return new Vector4M(1, 1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector4M Negate(Vector4M ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector4M Add(Vector4M ALeft, Vector4M ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector4M Subtract(Vector4M ALeft, Vector4M ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector4M Scale(Vector4M ALeft, decimal ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector4M Mask(Vector4M ALeft, Vector4M ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static decimal ScalarProduct(Vector4M ALeft, Vector4M ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2]
				 + ALeft.FElements[3]*ARight.FElements[3];
        }
        #endregion

        public Vector4M()
			: base(4)
		{ }
		public Vector4M(decimal AX, decimal AY, decimal AZ, decimal AW)
			: base(new []{AX, AY, AZ, AW}, false)
		{ }

		[Pure]
        public new Vector4M Clone()
        {
            return new Vector4M(this.FElements[0], this.FElements[1], this.FElements[2], this.FElements[3]);
        }

		#region IEquatable<Vector4M>
		[Pure]
		public bool Equals(Vector4M AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2]
				 && this.FElements[3] == AOther.FElements[3];
		}
		#endregion

		#region IAssignable<Vector4M>
		public void Assign(Vector4M AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
			this.FElements[3] = AFrom.FElements[3];
		}
		#endregion

		#region Mutating chainable operators
        public Vector4M Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
			this.FElements[3] = -this.FElements[3];
            return this;
        }
        public Vector4M Add(Vector4M ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
			this.FElements[3] += ARight.FElements[3];
            return this;
        }
        public Vector4M Scale(decimal ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
			this.FElements[3] *= ARight;
            return this;
        }
        public Vector4M Mask(Vector4M ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
			this.FElements[2] *= ARight.FElements[2];
			this.FElements[3] *= ARight.FElements[3];
            return this;
        }
        #endregion

		#region System.Object overrides
		[Pure]
        public override bool Equals(object AOther)
        {
            if (AOther is Vector4M)
                return this.Equals((Vector4M)AOther);

            return base.Equals(AOther);
        }
		[Pure]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
        #endregion

		public decimal LengthSq
		{
			get { return Vector4M.ScalarProduct(this, this); }
		}

		public decimal X
		{
			[Pure]
			get { return this.FElements[0]; }
			set { this.FElements[0] = value; }
		}
		public decimal Y
		{
			[Pure]
			get { return this.FElements[1]; }
			set { this.FElements[1] = value; }
		}
		public decimal Z
		{
			[Pure]
			get { return this.FElements[2]; }
			set { this.FElements[2] = value; }
		}
		public decimal W
		{
			[Pure]
			get { return this.FElements[3]; }
			set { this.FElements[3] = value; }
		}

		#region Static operator overloads
        public static bool operator ==(Vector4M ALeft, Vector4M ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector4M ALeft, Vector4M ARight)
        {
            if (object.ReferenceEquals(ALeft, null) || object.ReferenceEquals(ARight, null))
                return true;

            return !ALeft.Equals(ARight);
        }

        public static Vector4M operator -(Vector4M ALeft)
        {
            return Vector4M.Negate(ALeft);
        }
        public static Vector4M operator +(Vector4M ALeft, Vector4M ARight)
        {
            return Vector4M.Add(ALeft, ARight);
        }
        public static Vector4M operator -(Vector4M ALeft, Vector4M ARight)
        {
            return Vector4M.Subtract(ALeft, ARight);
        }
        public static Vector4M operator *(Vector4M ALeft, decimal ARight)
        {
            return Vector4M.Scale(ALeft, ARight);
        }
        #endregion
    }
}