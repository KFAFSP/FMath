using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Generic.Mutable;

namespace FMath.Linear.Numeric
{
    public sealed class Vector2Int :
		DenseVector<int>,
		IEquatable<Vector2Int>,
		IAssignable<Vector2Int>
	{
		#region Static factories
        public static Vector2Int Zero { get { return new Vector2Int(0, 0); } }
        public static Vector2Int One { get { return new Vector2Int(1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector2Int Negate(Vector2Int ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector2Int Add(Vector2Int ALeft, Vector2Int ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector2Int Subtract(Vector2Int ALeft, Vector2Int ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector2Int Scale(Vector2Int ALeft, int ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector2Int Mask(Vector2Int ALeft, Vector2Int ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static int ScalarProduct(Vector2Int ALeft, Vector2Int ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1];
        }
        #endregion

        public Vector2Int()
			: base(2)
		{ }
		public Vector2Int(int AX, int AY)
			: base(new []{AX, AY}, false)
		{ }

		[Pure]
        public new Vector2Int Clone()
        {
            return new Vector2Int(this.FElements[0], this.FElements[1]);
        }

		#region IEquatable<Vector2Int>
		public bool Equals(Vector2Int AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1];
		}
		#endregion

		#region IAssignable<Vector2Int>
		public void Assign(Vector2Int AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
		}
		#endregion

		#region Mutating chainable operators
        public Vector2Int Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
            return this;
        }
        public Vector2Int Add(Vector2Int ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
            return this;
        }
        public Vector2Int Scale(int ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
            return this;
        }
        public Vector2Int Mask(Vector2Int ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
            return this;
        }
        #endregion

		#region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther is Vector2Int)
                return this.Equals((Vector2Int)AOther);

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

		#region Static operator overloads
        public static bool operator ==(Vector2Int ALeft, Vector2Int ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector2Int ALeft, Vector2Int ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector2Int operator -(Vector2Int ALeft)
        {
            return Vector2Int.Negate(ALeft);
        }
        public static Vector2Int operator +(Vector2Int ALeft, Vector2Int ARight)
        {
            return Vector2Int.Add(ALeft, ARight);
        }
        public static Vector2Int operator -(Vector2Int ALeft, Vector2Int ARight)
        {
            return Vector2Int.Subtract(ALeft, ARight);
        }
        public static Vector2Int operator *(Vector2Int ALeft, int ARight)
        {
            return Vector2Int.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector2Flt :
		DenseVector<float>,
		IEquatable<Vector2Flt>,
		IAssignable<Vector2Flt>
	{
		#region Static factories
        public static Vector2Flt Zero { get { return new Vector2Flt(0, 0); } }
        public static Vector2Flt One { get { return new Vector2Flt(1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector2Flt Negate(Vector2Flt ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector2Flt Add(Vector2Flt ALeft, Vector2Flt ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector2Flt Subtract(Vector2Flt ALeft, Vector2Flt ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector2Flt Scale(Vector2Flt ALeft, float ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector2Flt Mask(Vector2Flt ALeft, Vector2Flt ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static float ScalarProduct(Vector2Flt ALeft, Vector2Flt ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1];
        }
        #endregion

        public Vector2Flt()
			: base(2)
		{ }
		public Vector2Flt(float AX, float AY)
			: base(new []{AX, AY}, false)
		{ }

		[Pure]
        public new Vector2Flt Clone()
        {
            return new Vector2Flt(this.FElements[0], this.FElements[1]);
        }

		#region IEquatable<Vector2Flt>
		public bool Equals(Vector2Flt AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1];
		}
		#endregion

		#region IAssignable<Vector2Flt>
		public void Assign(Vector2Flt AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
		}
		#endregion

		#region Mutating chainable operators
        public Vector2Flt Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
            return this;
        }
        public Vector2Flt Add(Vector2Flt ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
            return this;
        }
        public Vector2Flt Scale(float ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
            return this;
        }
        public Vector2Flt Mask(Vector2Flt ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
            return this;
        }
        #endregion

		#region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther is Vector2Flt)
                return this.Equals((Vector2Flt)AOther);

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

		#region Static operator overloads
        public static bool operator ==(Vector2Flt ALeft, Vector2Flt ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector2Flt ALeft, Vector2Flt ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector2Flt operator -(Vector2Flt ALeft)
        {
            return Vector2Flt.Negate(ALeft);
        }
        public static Vector2Flt operator +(Vector2Flt ALeft, Vector2Flt ARight)
        {
            return Vector2Flt.Add(ALeft, ARight);
        }
        public static Vector2Flt operator -(Vector2Flt ALeft, Vector2Flt ARight)
        {
            return Vector2Flt.Subtract(ALeft, ARight);
        }
        public static Vector2Flt operator *(Vector2Flt ALeft, float ARight)
        {
            return Vector2Flt.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector2Dbl :
		DenseVector<double>,
		IEquatable<Vector2Dbl>,
		IAssignable<Vector2Dbl>
	{
		#region Static factories
        public static Vector2Dbl Zero { get { return new Vector2Dbl(0, 0); } }
        public static Vector2Dbl One { get { return new Vector2Dbl(1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector2Dbl Negate(Vector2Dbl ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector2Dbl Add(Vector2Dbl ALeft, Vector2Dbl ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector2Dbl Subtract(Vector2Dbl ALeft, Vector2Dbl ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector2Dbl Scale(Vector2Dbl ALeft, double ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector2Dbl Mask(Vector2Dbl ALeft, Vector2Dbl ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static double ScalarProduct(Vector2Dbl ALeft, Vector2Dbl ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1];
        }
        #endregion

        public Vector2Dbl()
			: base(2)
		{ }
		public Vector2Dbl(double AX, double AY)
			: base(new []{AX, AY}, false)
		{ }

		[Pure]
        public new Vector2Dbl Clone()
        {
            return new Vector2Dbl(this.FElements[0], this.FElements[1]);
        }

		#region IEquatable<Vector2Dbl>
		public bool Equals(Vector2Dbl AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1];
		}
		#endregion

		#region IAssignable<Vector2Dbl>
		public void Assign(Vector2Dbl AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
		}
		#endregion

		#region Mutating chainable operators
        public Vector2Dbl Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
            return this;
        }
        public Vector2Dbl Add(Vector2Dbl ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
            return this;
        }
        public Vector2Dbl Scale(double ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
            return this;
        }
        public Vector2Dbl Mask(Vector2Dbl ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
            return this;
        }
        #endregion

		#region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther is Vector2Dbl)
                return this.Equals((Vector2Dbl)AOther);

            return base.Equals(AOther);
        }
        #endregion

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
        public static bool operator ==(Vector2Dbl ALeft, Vector2Dbl ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector2Dbl ALeft, Vector2Dbl ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector2Dbl operator -(Vector2Dbl ALeft)
        {
            return Vector2Dbl.Negate(ALeft);
        }
        public static Vector2Dbl operator +(Vector2Dbl ALeft, Vector2Dbl ARight)
        {
            return Vector2Dbl.Add(ALeft, ARight);
        }
        public static Vector2Dbl operator -(Vector2Dbl ALeft, Vector2Dbl ARight)
        {
            return Vector2Dbl.Subtract(ALeft, ARight);
        }
        public static Vector2Dbl operator *(Vector2Dbl ALeft, double ARight)
        {
            return Vector2Dbl.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector2Dcm :
		DenseVector<decimal>,
		IEquatable<Vector2Dcm>,
		IAssignable<Vector2Dcm>
	{
		#region Static factories
        public static Vector2Dcm Zero { get { return new Vector2Dcm(0, 0); } }
        public static Vector2Dcm One { get { return new Vector2Dcm(1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector2Dcm Negate(Vector2Dcm ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector2Dcm Add(Vector2Dcm ALeft, Vector2Dcm ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector2Dcm Subtract(Vector2Dcm ALeft, Vector2Dcm ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector2Dcm Scale(Vector2Dcm ALeft, decimal ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector2Dcm Mask(Vector2Dcm ALeft, Vector2Dcm ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static decimal ScalarProduct(Vector2Dcm ALeft, Vector2Dcm ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1];
        }
        #endregion

        public Vector2Dcm()
			: base(2)
		{ }
		public Vector2Dcm(decimal AX, decimal AY)
			: base(new []{AX, AY}, false)
		{ }

		[Pure]
        public new Vector2Dcm Clone()
        {
            return new Vector2Dcm(this.FElements[0], this.FElements[1]);
        }

		#region IEquatable<Vector2Dcm>
		public bool Equals(Vector2Dcm AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1];
		}
		#endregion

		#region IAssignable<Vector2Dcm>
		public void Assign(Vector2Dcm AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
		}
		#endregion

		#region Mutating chainable operators
        public Vector2Dcm Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
            return this;
        }
        public Vector2Dcm Add(Vector2Dcm ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
            return this;
        }
        public Vector2Dcm Scale(decimal ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
            return this;
        }
        public Vector2Dcm Mask(Vector2Dcm ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] *= ARight.FElements[0];
			this.FElements[1] *= ARight.FElements[1];
            return this;
        }
        #endregion

		#region System.Object overrides
        public override bool Equals(object AOther)
        {
            if (AOther is Vector2Dcm)
                return this.Equals((Vector2Dcm)AOther);

            return base.Equals(AOther);
        }
        #endregion

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
        public static bool operator ==(Vector2Dcm ALeft, Vector2Dcm ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector2Dcm ALeft, Vector2Dcm ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector2Dcm operator -(Vector2Dcm ALeft)
        {
            return Vector2Dcm.Negate(ALeft);
        }
        public static Vector2Dcm operator +(Vector2Dcm ALeft, Vector2Dcm ARight)
        {
            return Vector2Dcm.Add(ALeft, ARight);
        }
        public static Vector2Dcm operator -(Vector2Dcm ALeft, Vector2Dcm ARight)
        {
            return Vector2Dcm.Subtract(ALeft, ARight);
        }
        public static Vector2Dcm operator *(Vector2Dcm ALeft, decimal ARight)
        {
            return Vector2Dcm.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector3Int :
		DenseVector<int>,
		IEquatable<Vector3Int>,
		IAssignable<Vector3Int>
	{
		#region Static factories
        public static Vector3Int Zero { get { return new Vector3Int(0, 0, 0); } }
        public static Vector3Int One { get { return new Vector3Int(1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector3Int Negate(Vector3Int ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector3Int Add(Vector3Int ALeft, Vector3Int ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector3Int Subtract(Vector3Int ALeft, Vector3Int ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector3Int Scale(Vector3Int ALeft, int ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector3Int Mask(Vector3Int ALeft, Vector3Int ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static int ScalarProduct(Vector3Int ALeft, Vector3Int ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2];
        }
		[Pure]
        public static Vector3Int VectorProduct(Vector3Int ALeft, Vector3Int ARight)
        {
            return new Vector3Int(
                ALeft.FElements[1] * ARight.FElements[2] - ALeft.FElements[2] * ARight.FElements[1],
                ALeft.FElements[2] * ARight.FElements[0] - ALeft.FElements[0] * ARight.FElements[2],
                ALeft.FElements[0] * ARight.FElements[1] - ALeft.FElements[1] * ARight.FElements[0]);
        }
        #endregion

        public Vector3Int()
			: base(3)
		{ }
		public Vector3Int(int AX, int AY, int AZ)
			: base(new []{AX, AY, AZ}, false)
		{ }

		[Pure]
        public new Vector3Int Clone()
        {
            return new Vector3Int(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

		#region IEquatable<Vector3Int>
		public bool Equals(Vector3Int AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2];
		}
		#endregion

		#region IAssignable<Vector3Int>
		public void Assign(Vector3Int AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
		}
		#endregion

		#region Mutating chainable operators
        public Vector3Int Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
            return this;
        }
        public Vector3Int Add(Vector3Int ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
            return this;
        }
        public Vector3Int Scale(int ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
            return this;
        }
        public Vector3Int Mask(Vector3Int ARight)
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
        public override bool Equals(object AOther)
        {
            if (AOther is Vector3Int)
                return this.Equals((Vector3Int)AOther);

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
        public static bool operator ==(Vector3Int ALeft, Vector3Int ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector3Int ALeft, Vector3Int ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector3Int operator -(Vector3Int ALeft)
        {
            return Vector3Int.Negate(ALeft);
        }
        public static Vector3Int operator +(Vector3Int ALeft, Vector3Int ARight)
        {
            return Vector3Int.Add(ALeft, ARight);
        }
        public static Vector3Int operator -(Vector3Int ALeft, Vector3Int ARight)
        {
            return Vector3Int.Subtract(ALeft, ARight);
        }
        public static Vector3Int operator *(Vector3Int ALeft, int ARight)
        {
            return Vector3Int.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector3Flt :
		DenseVector<float>,
		IEquatable<Vector3Flt>,
		IAssignable<Vector3Flt>
	{
		#region Static factories
        public static Vector3Flt Zero { get { return new Vector3Flt(0, 0, 0); } }
        public static Vector3Flt One { get { return new Vector3Flt(1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector3Flt Negate(Vector3Flt ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector3Flt Add(Vector3Flt ALeft, Vector3Flt ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector3Flt Subtract(Vector3Flt ALeft, Vector3Flt ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector3Flt Scale(Vector3Flt ALeft, float ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector3Flt Mask(Vector3Flt ALeft, Vector3Flt ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static float ScalarProduct(Vector3Flt ALeft, Vector3Flt ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2];
        }
		[Pure]
        public static Vector3Flt VectorProduct(Vector3Flt ALeft, Vector3Flt ARight)
        {
            return new Vector3Flt(
                ALeft.FElements[1] * ARight.FElements[2] - ALeft.FElements[2] * ARight.FElements[1],
                ALeft.FElements[2] * ARight.FElements[0] - ALeft.FElements[0] * ARight.FElements[2],
                ALeft.FElements[0] * ARight.FElements[1] - ALeft.FElements[1] * ARight.FElements[0]);
        }
        #endregion

        public Vector3Flt()
			: base(3)
		{ }
		public Vector3Flt(float AX, float AY, float AZ)
			: base(new []{AX, AY, AZ}, false)
		{ }

		[Pure]
        public new Vector3Flt Clone()
        {
            return new Vector3Flt(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

		#region IEquatable<Vector3Flt>
		public bool Equals(Vector3Flt AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2];
		}
		#endregion

		#region IAssignable<Vector3Flt>
		public void Assign(Vector3Flt AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
		}
		#endregion

		#region Mutating chainable operators
        public Vector3Flt Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
            return this;
        }
        public Vector3Flt Add(Vector3Flt ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
            return this;
        }
        public Vector3Flt Scale(float ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
            return this;
        }
        public Vector3Flt Mask(Vector3Flt ARight)
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
        public override bool Equals(object AOther)
        {
            if (AOther is Vector3Flt)
                return this.Equals((Vector3Flt)AOther);

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
        public static bool operator ==(Vector3Flt ALeft, Vector3Flt ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector3Flt ALeft, Vector3Flt ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector3Flt operator -(Vector3Flt ALeft)
        {
            return Vector3Flt.Negate(ALeft);
        }
        public static Vector3Flt operator +(Vector3Flt ALeft, Vector3Flt ARight)
        {
            return Vector3Flt.Add(ALeft, ARight);
        }
        public static Vector3Flt operator -(Vector3Flt ALeft, Vector3Flt ARight)
        {
            return Vector3Flt.Subtract(ALeft, ARight);
        }
        public static Vector3Flt operator *(Vector3Flt ALeft, float ARight)
        {
            return Vector3Flt.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector3Dbl :
		DenseVector<double>,
		IEquatable<Vector3Dbl>,
		IAssignable<Vector3Dbl>
	{
		#region Static factories
        public static Vector3Dbl Zero { get { return new Vector3Dbl(0, 0, 0); } }
        public static Vector3Dbl One { get { return new Vector3Dbl(1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector3Dbl Negate(Vector3Dbl ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector3Dbl Add(Vector3Dbl ALeft, Vector3Dbl ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector3Dbl Subtract(Vector3Dbl ALeft, Vector3Dbl ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector3Dbl Scale(Vector3Dbl ALeft, double ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector3Dbl Mask(Vector3Dbl ALeft, Vector3Dbl ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static double ScalarProduct(Vector3Dbl ALeft, Vector3Dbl ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2];
        }
		[Pure]
        public static Vector3Dbl VectorProduct(Vector3Dbl ALeft, Vector3Dbl ARight)
        {
            return new Vector3Dbl(
                ALeft.FElements[1] * ARight.FElements[2] - ALeft.FElements[2] * ARight.FElements[1],
                ALeft.FElements[2] * ARight.FElements[0] - ALeft.FElements[0] * ARight.FElements[2],
                ALeft.FElements[0] * ARight.FElements[1] - ALeft.FElements[1] * ARight.FElements[0]);
        }
        #endregion

        public Vector3Dbl()
			: base(3)
		{ }
		public Vector3Dbl(double AX, double AY, double AZ)
			: base(new []{AX, AY, AZ}, false)
		{ }

		[Pure]
        public new Vector3Dbl Clone()
        {
            return new Vector3Dbl(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

		#region IEquatable<Vector3Dbl>
		public bool Equals(Vector3Dbl AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2];
		}
		#endregion

		#region IAssignable<Vector3Dbl>
		public void Assign(Vector3Dbl AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
		}
		#endregion

		#region Mutating chainable operators
        public Vector3Dbl Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
            return this;
        }
        public Vector3Dbl Add(Vector3Dbl ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
            return this;
        }
        public Vector3Dbl Scale(double ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
            return this;
        }
        public Vector3Dbl Mask(Vector3Dbl ARight)
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
        public override bool Equals(object AOther)
        {
            if (AOther is Vector3Dbl)
                return this.Equals((Vector3Dbl)AOther);

            return base.Equals(AOther);
        }
        #endregion

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
        public static bool operator ==(Vector3Dbl ALeft, Vector3Dbl ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector3Dbl ALeft, Vector3Dbl ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector3Dbl operator -(Vector3Dbl ALeft)
        {
            return Vector3Dbl.Negate(ALeft);
        }
        public static Vector3Dbl operator +(Vector3Dbl ALeft, Vector3Dbl ARight)
        {
            return Vector3Dbl.Add(ALeft, ARight);
        }
        public static Vector3Dbl operator -(Vector3Dbl ALeft, Vector3Dbl ARight)
        {
            return Vector3Dbl.Subtract(ALeft, ARight);
        }
        public static Vector3Dbl operator *(Vector3Dbl ALeft, double ARight)
        {
            return Vector3Dbl.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector3Dcm :
		DenseVector<decimal>,
		IEquatable<Vector3Dcm>,
		IAssignable<Vector3Dcm>
	{
		#region Static factories
        public static Vector3Dcm Zero { get { return new Vector3Dcm(0, 0, 0); } }
        public static Vector3Dcm One { get { return new Vector3Dcm(1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector3Dcm Negate(Vector3Dcm ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector3Dcm Add(Vector3Dcm ALeft, Vector3Dcm ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector3Dcm Subtract(Vector3Dcm ALeft, Vector3Dcm ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector3Dcm Scale(Vector3Dcm ALeft, decimal ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector3Dcm Mask(Vector3Dcm ALeft, Vector3Dcm ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static decimal ScalarProduct(Vector3Dcm ALeft, Vector3Dcm ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2];
        }
		[Pure]
        public static Vector3Dcm VectorProduct(Vector3Dcm ALeft, Vector3Dcm ARight)
        {
            return new Vector3Dcm(
                ALeft.FElements[1] * ARight.FElements[2] - ALeft.FElements[2] * ARight.FElements[1],
                ALeft.FElements[2] * ARight.FElements[0] - ALeft.FElements[0] * ARight.FElements[2],
                ALeft.FElements[0] * ARight.FElements[1] - ALeft.FElements[1] * ARight.FElements[0]);
        }
        #endregion

        public Vector3Dcm()
			: base(3)
		{ }
		public Vector3Dcm(decimal AX, decimal AY, decimal AZ)
			: base(new []{AX, AY, AZ}, false)
		{ }

		[Pure]
        public new Vector3Dcm Clone()
        {
            return new Vector3Dcm(this.FElements[0], this.FElements[1], this.FElements[2]);
        }

		#region IEquatable<Vector3Dcm>
		public bool Equals(Vector3Dcm AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2];
		}
		#endregion

		#region IAssignable<Vector3Dcm>
		public void Assign(Vector3Dcm AFrom)
		{
			if (AFrom == null)
				throw new ArgumentNullException("AFrom");

			this.FElements[0] = AFrom.FElements[0];
			this.FElements[1] = AFrom.FElements[1];
			this.FElements[2] = AFrom.FElements[2];
		}
		#endregion

		#region Mutating chainable operators
        public Vector3Dcm Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
            return this;
        }
        public Vector3Dcm Add(Vector3Dcm ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
            return this;
        }
        public Vector3Dcm Scale(decimal ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
            return this;
        }
        public Vector3Dcm Mask(Vector3Dcm ARight)
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
        public override bool Equals(object AOther)
        {
            if (AOther is Vector3Dcm)
                return this.Equals((Vector3Dcm)AOther);

            return base.Equals(AOther);
        }
        #endregion

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
        public static bool operator ==(Vector3Dcm ALeft, Vector3Dcm ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector3Dcm ALeft, Vector3Dcm ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector3Dcm operator -(Vector3Dcm ALeft)
        {
            return Vector3Dcm.Negate(ALeft);
        }
        public static Vector3Dcm operator +(Vector3Dcm ALeft, Vector3Dcm ARight)
        {
            return Vector3Dcm.Add(ALeft, ARight);
        }
        public static Vector3Dcm operator -(Vector3Dcm ALeft, Vector3Dcm ARight)
        {
            return Vector3Dcm.Subtract(ALeft, ARight);
        }
        public static Vector3Dcm operator *(Vector3Dcm ALeft, decimal ARight)
        {
            return Vector3Dcm.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector4Int :
		DenseVector<int>,
		IEquatable<Vector4Int>,
		IAssignable<Vector4Int>
	{
		#region Static factories
        public static Vector4Int Zero { get { return new Vector4Int(0, 0, 0, 0); } }
        public static Vector4Int One { get { return new Vector4Int(1, 1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector4Int Negate(Vector4Int ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector4Int Add(Vector4Int ALeft, Vector4Int ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector4Int Subtract(Vector4Int ALeft, Vector4Int ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector4Int Scale(Vector4Int ALeft, int ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector4Int Mask(Vector4Int ALeft, Vector4Int ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static int ScalarProduct(Vector4Int ALeft, Vector4Int ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2]
				 + ALeft.FElements[3]*ARight.FElements[3];
        }
        #endregion

        public Vector4Int()
			: base(4)
		{ }
		public Vector4Int(int AX, int AY, int AZ, int AW)
			: base(new []{AX, AY, AZ, AW}, false)
		{ }

		[Pure]
        public new Vector4Int Clone()
        {
            return new Vector4Int(this.FElements[0], this.FElements[1], this.FElements[2], this.FElements[3]);
        }

		#region IEquatable<Vector4Int>
		public bool Equals(Vector4Int AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2]
				 && this.FElements[3] == AOther.FElements[3];
		}
		#endregion

		#region IAssignable<Vector4Int>
		public void Assign(Vector4Int AFrom)
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
        public Vector4Int Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
			this.FElements[3] = -this.FElements[3];
            return this;
        }
        public Vector4Int Add(Vector4Int ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
			this.FElements[3] += ARight.FElements[3];
            return this;
        }
        public Vector4Int Scale(int ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
			this.FElements[3] *= ARight;
            return this;
        }
        public Vector4Int Mask(Vector4Int ARight)
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
        public override bool Equals(object AOther)
        {
            if (AOther is Vector4Int)
                return this.Equals((Vector4Int)AOther);

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
		public int W
		{
			[Pure]
			get { return this.FElements[3]; }
			set { this.FElements[3] = value; }
		}

		#region Static operator overloads
        public static bool operator ==(Vector4Int ALeft, Vector4Int ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector4Int ALeft, Vector4Int ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector4Int operator -(Vector4Int ALeft)
        {
            return Vector4Int.Negate(ALeft);
        }
        public static Vector4Int operator +(Vector4Int ALeft, Vector4Int ARight)
        {
            return Vector4Int.Add(ALeft, ARight);
        }
        public static Vector4Int operator -(Vector4Int ALeft, Vector4Int ARight)
        {
            return Vector4Int.Subtract(ALeft, ARight);
        }
        public static Vector4Int operator *(Vector4Int ALeft, int ARight)
        {
            return Vector4Int.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector4Flt :
		DenseVector<float>,
		IEquatable<Vector4Flt>,
		IAssignable<Vector4Flt>
	{
		#region Static factories
        public static Vector4Flt Zero { get { return new Vector4Flt(0, 0, 0, 0); } }
        public static Vector4Flt One { get { return new Vector4Flt(1, 1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector4Flt Negate(Vector4Flt ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector4Flt Add(Vector4Flt ALeft, Vector4Flt ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector4Flt Subtract(Vector4Flt ALeft, Vector4Flt ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector4Flt Scale(Vector4Flt ALeft, float ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector4Flt Mask(Vector4Flt ALeft, Vector4Flt ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static float ScalarProduct(Vector4Flt ALeft, Vector4Flt ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2]
				 + ALeft.FElements[3]*ARight.FElements[3];
        }
        #endregion

        public Vector4Flt()
			: base(4)
		{ }
		public Vector4Flt(float AX, float AY, float AZ, float AW)
			: base(new []{AX, AY, AZ, AW}, false)
		{ }

		[Pure]
        public new Vector4Flt Clone()
        {
            return new Vector4Flt(this.FElements[0], this.FElements[1], this.FElements[2], this.FElements[3]);
        }

		#region IEquatable<Vector4Flt>
		public bool Equals(Vector4Flt AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2]
				 && this.FElements[3] == AOther.FElements[3];
		}
		#endregion

		#region IAssignable<Vector4Flt>
		public void Assign(Vector4Flt AFrom)
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
        public Vector4Flt Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
			this.FElements[3] = -this.FElements[3];
            return this;
        }
        public Vector4Flt Add(Vector4Flt ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
			this.FElements[3] += ARight.FElements[3];
            return this;
        }
        public Vector4Flt Scale(float ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
			this.FElements[3] *= ARight;
            return this;
        }
        public Vector4Flt Mask(Vector4Flt ARight)
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
        public override bool Equals(object AOther)
        {
            if (AOther is Vector4Flt)
                return this.Equals((Vector4Flt)AOther);

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
		public float W
		{
			[Pure]
			get { return this.FElements[3]; }
			set { this.FElements[3] = value; }
		}

		#region Static operator overloads
        public static bool operator ==(Vector4Flt ALeft, Vector4Flt ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector4Flt ALeft, Vector4Flt ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector4Flt operator -(Vector4Flt ALeft)
        {
            return Vector4Flt.Negate(ALeft);
        }
        public static Vector4Flt operator +(Vector4Flt ALeft, Vector4Flt ARight)
        {
            return Vector4Flt.Add(ALeft, ARight);
        }
        public static Vector4Flt operator -(Vector4Flt ALeft, Vector4Flt ARight)
        {
            return Vector4Flt.Subtract(ALeft, ARight);
        }
        public static Vector4Flt operator *(Vector4Flt ALeft, float ARight)
        {
            return Vector4Flt.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector4Dbl :
		DenseVector<double>,
		IEquatable<Vector4Dbl>,
		IAssignable<Vector4Dbl>
	{
		#region Static factories
        public static Vector4Dbl Zero { get { return new Vector4Dbl(0, 0, 0, 0); } }
        public static Vector4Dbl One { get { return new Vector4Dbl(1, 1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector4Dbl Negate(Vector4Dbl ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector4Dbl Add(Vector4Dbl ALeft, Vector4Dbl ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector4Dbl Subtract(Vector4Dbl ALeft, Vector4Dbl ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector4Dbl Scale(Vector4Dbl ALeft, double ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector4Dbl Mask(Vector4Dbl ALeft, Vector4Dbl ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static double ScalarProduct(Vector4Dbl ALeft, Vector4Dbl ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2]
				 + ALeft.FElements[3]*ARight.FElements[3];
        }
        #endregion

        public Vector4Dbl()
			: base(4)
		{ }
		public Vector4Dbl(double AX, double AY, double AZ, double AW)
			: base(new []{AX, AY, AZ, AW}, false)
		{ }

		[Pure]
        public new Vector4Dbl Clone()
        {
            return new Vector4Dbl(this.FElements[0], this.FElements[1], this.FElements[2], this.FElements[3]);
        }

		#region IEquatable<Vector4Dbl>
		public bool Equals(Vector4Dbl AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2]
				 && this.FElements[3] == AOther.FElements[3];
		}
		#endregion

		#region IAssignable<Vector4Dbl>
		public void Assign(Vector4Dbl AFrom)
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
        public Vector4Dbl Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
			this.FElements[3] = -this.FElements[3];
            return this;
        }
        public Vector4Dbl Add(Vector4Dbl ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
			this.FElements[3] += ARight.FElements[3];
            return this;
        }
        public Vector4Dbl Scale(double ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
			this.FElements[3] *= ARight;
            return this;
        }
        public Vector4Dbl Mask(Vector4Dbl ARight)
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
        public override bool Equals(object AOther)
        {
            if (AOther is Vector4Dbl)
                return this.Equals((Vector4Dbl)AOther);

            return base.Equals(AOther);
        }
        #endregion

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
        public static bool operator ==(Vector4Dbl ALeft, Vector4Dbl ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector4Dbl ALeft, Vector4Dbl ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector4Dbl operator -(Vector4Dbl ALeft)
        {
            return Vector4Dbl.Negate(ALeft);
        }
        public static Vector4Dbl operator +(Vector4Dbl ALeft, Vector4Dbl ARight)
        {
            return Vector4Dbl.Add(ALeft, ARight);
        }
        public static Vector4Dbl operator -(Vector4Dbl ALeft, Vector4Dbl ARight)
        {
            return Vector4Dbl.Subtract(ALeft, ARight);
        }
        public static Vector4Dbl operator *(Vector4Dbl ALeft, double ARight)
        {
            return Vector4Dbl.Scale(ALeft, ARight);
        }
        #endregion
    }

    public sealed class Vector4Dcm :
		DenseVector<decimal>,
		IEquatable<Vector4Dcm>,
		IAssignable<Vector4Dcm>
	{
		#region Static factories
        public static Vector4Dcm Zero { get { return new Vector4Dcm(0, 0, 0, 0); } }
        public static Vector4Dcm One { get { return new Vector4Dcm(1, 1, 1, 1); } }
        #endregion

		#region Pure static operators
        [Pure]
        public static Vector4Dcm Negate(Vector4Dcm ALeft)
        {
            return ALeft.Clone().Negate();
        }
        [Pure]
        public static Vector4Dcm Add(Vector4Dcm ALeft, Vector4Dcm ARight)
        {
            return ALeft.Clone().Add(ARight);
        }
        [Pure]
        public static Vector4Dcm Subtract(Vector4Dcm ALeft, Vector4Dcm ARight)
        {
            return ARight.Clone().Negate().Add(ALeft);
        }
        [Pure]
        public static Vector4Dcm Scale(Vector4Dcm ALeft, decimal ARight)
        {
            return ALeft.Clone().Scale(ARight);
        }
        [Pure]
        public static Vector4Dcm Mask(Vector4Dcm ALeft, Vector4Dcm ARight)
        {
            return ALeft.Clone().Mask(ARight);
        }

        [Pure]
        public static decimal ScalarProduct(Vector4Dcm ALeft, Vector4Dcm ARight)
        {
            return ALeft.FElements[0]*ARight.FElements[0]
				 + ALeft.FElements[1]*ARight.FElements[1]
				 + ALeft.FElements[2]*ARight.FElements[2]
				 + ALeft.FElements[3]*ARight.FElements[3];
        }
        #endregion

        public Vector4Dcm()
			: base(4)
		{ }
		public Vector4Dcm(decimal AX, decimal AY, decimal AZ, decimal AW)
			: base(new []{AX, AY, AZ, AW}, false)
		{ }

		[Pure]
        public new Vector4Dcm Clone()
        {
            return new Vector4Dcm(this.FElements[0], this.FElements[1], this.FElements[2], this.FElements[3]);
        }

		#region IEquatable<Vector4Dcm>
		public bool Equals(Vector4Dcm AOther)
		{
			if (AOther == null)
				return false;

			return this.FElements[0] == AOther.FElements[0]
				 && this.FElements[1] == AOther.FElements[1]
				 && this.FElements[2] == AOther.FElements[2]
				 && this.FElements[3] == AOther.FElements[3];
		}
		#endregion

		#region IAssignable<Vector4Dcm>
		public void Assign(Vector4Dcm AFrom)
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
        public Vector4Dcm Negate()
        {
			this.FElements[0] = -this.FElements[0];
			this.FElements[1] = -this.FElements[1];
			this.FElements[2] = -this.FElements[2];
			this.FElements[3] = -this.FElements[3];
            return this;
        }
        public Vector4Dcm Add(Vector4Dcm ARight)
        {
            if (ARight == null)
                throw new ArgumentNullException("ARight");

			this.FElements[0] += ARight.FElements[0];
			this.FElements[1] += ARight.FElements[1];
			this.FElements[2] += ARight.FElements[2];
			this.FElements[3] += ARight.FElements[3];
            return this;
        }
        public Vector4Dcm Scale(decimal ARight)
        {
			this.FElements[0] *= ARight;
			this.FElements[1] *= ARight;
			this.FElements[2] *= ARight;
			this.FElements[3] *= ARight;
            return this;
        }
        public Vector4Dcm Mask(Vector4Dcm ARight)
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
        public override bool Equals(object AOther)
        {
            if (AOther is Vector4Dcm)
                return this.Equals((Vector4Dcm)AOther);

            return base.Equals(AOther);
        }
        #endregion

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
        public static bool operator ==(Vector4Dcm ALeft, Vector4Dcm ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return ALeft.Equals(ARight);
        }
        public static bool operator !=(Vector4Dcm ALeft, Vector4Dcm ARight)
        {
            if (ALeft == null || ARight == null)
                return false;

            return !ALeft.Equals(ARight);
        }

        public static Vector4Dcm operator -(Vector4Dcm ALeft)
        {
            return Vector4Dcm.Negate(ALeft);
        }
        public static Vector4Dcm operator +(Vector4Dcm ALeft, Vector4Dcm ARight)
        {
            return Vector4Dcm.Add(ALeft, ARight);
        }
        public static Vector4Dcm operator -(Vector4Dcm ALeft, Vector4Dcm ARight)
        {
            return Vector4Dcm.Subtract(ALeft, ARight);
        }
        public static Vector4Dcm operator *(Vector4Dcm ALeft, decimal ARight)
        {
            return Vector4Dcm.Scale(ALeft, ARight);
        }
        #endregion
    }
}