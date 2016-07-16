using System;

namespace FMath.Arithmetic
{
    public abstract class FractionalArithmeticProvider<TNumeral> :
        IntegerArithmeticProvider<TNumeral>
    {
        public sealed class Float : FractionalArithmeticProvider<float>
        {
            public override float Add(float ALeft, float ARight)
            {
                return ALeft + ARight;
            }
            public override float Multiply(float ALeft, float ARight)
            {
                return ALeft * ARight;
            }
            public override float Div(float ALeft, float ARight)
            {
                return (float)Math.Floor(ALeft / ARight);
            }
            public override float Mod(float ALeft, float ARight)
            {
                return ALeft % ARight;
            }

            public override bool IsModular { get { return false; } }
            public override float Zero { get { return 0.0f; } }
            public override float One { get { return 1.0f; } }

            public override float Sign(float ALeft)
            {
                return Math.Sign(ALeft);
            }
            public override float Absolute(float ALeft)
            {
                return Math.Abs(ALeft);
            }
            public override float Negate(float ALeft)
            {
                return -ALeft;
            }

            public override float Invert(float ALeft)
            {
                return 1.0f/ALeft;
            }
            public override float Ceil(float ALeft)
            {
                return (float)Math.Ceiling(ALeft);
            }
            public override float Trunc(float ALeft)
            {
                return (float)Math.Truncate(ALeft);
            }
            public override float Round(float ALeft, MidpointRounding AMidpointStrategy)
            {
                return (float)Math.Round(ALeft, AMidpointStrategy);
            }
            public override float Frac(float ALeft)
            {
                return ALeft - this.Trunc(ALeft);
            }
            public override float Divide(float ALeft, float ARight)
            {
                return ALeft/ARight;
            }

            public override float Infinity { get { return float.PositiveInfinity; } }
            public override float NaN { get { return float.NaN; } }

            public override NumeralType Type { get { return NumeralType.Fractional; } }
        }

        public sealed class Double : FractionalArithmeticProvider<double>
        {
            public override double Add(double ALeft, double ARight)
            {
                return ALeft + ARight;
            }
            public override double Multiply(double ALeft, double ARight)
            {
                return ALeft * ARight;
            }
            public override double Div(double ALeft, double ARight)
            {
                return Math.Floor(ALeft / ARight);
            }
            public override double Mod(double ALeft, double ARight)
            {
                return ALeft % ARight;
            }

            public override bool IsModular { get { return false; } }
            public override double Zero { get { return 0.0d; } }
            public override double One { get { return 1.0d; } }

            public override double Sign(double ALeft)
            {
                return Math.Sign(ALeft);
            }
            public override double Absolute(double ALeft)
            {
                return Math.Abs(ALeft);
            }
            public override double Negate(double ALeft)
            {
                return -ALeft;
            }

            public override double Invert(double ALeft)
            {
                return 1.0d / ALeft;
            }
            public override double Ceil(double ALeft)
            {
                return Math.Ceiling(ALeft);
            }
            public override double Trunc(double ALeft)
            {
                return Math.Truncate(ALeft);
            }
            public override double Round(double ALeft, MidpointRounding AMidpointStrategy)
            {
                return Math.Round(ALeft, AMidpointStrategy);
            }
            public override double Frac(double ALeft)
            {
                return ALeft - this.Trunc(ALeft);
            }
            public override double Divide(double ALeft, double ARight)
            {
                return ALeft / ARight;
            }

            public override double Infinity { get { return double.PositiveInfinity; } }
            public override double NaN { get { return double.NaN; } }

            public override NumeralType Type { get { return NumeralType.Fractional; } }
        }

        public sealed class Decimal : FractionalArithmeticProvider<decimal>
        {
            public override decimal Add(decimal ALeft, decimal ARight)
            {
                return ALeft + ARight;
            }
            public override decimal Multiply(decimal ALeft, decimal ARight)
            {
                return ALeft * ARight;
            }
            public override decimal Div(decimal ALeft, decimal ARight)
            {
                return Math.Truncate(ALeft / ARight);
            }
            public override decimal Mod(decimal ALeft, decimal ARight)
            {
                return ALeft % ARight;
            }

            public override bool IsModular { get { return false; } }
            public override decimal Zero { get { return 0.0m; } }
            public override decimal One { get { return 1.0m; } }

            public override decimal Sign(decimal ALeft)
            {
                return Math.Sign(ALeft);
            }
            public override decimal Absolute(decimal ALeft)
            {
                return Math.Abs(ALeft);
            }
            public override decimal Negate(decimal ALeft)
            {
                return -ALeft;
            }

            public override decimal Invert(decimal ALeft)
            {
                return 1.0m / ALeft;
            }
            public override decimal Ceil(decimal ALeft)
            {
                return Math.Ceiling(ALeft);
            }
            public override decimal Trunc(decimal ALeft)
            {
                return Math.Truncate(ALeft);
            }
            public override decimal Round(decimal ALeft, MidpointRounding AMidpointStrategy)
            {
                return Math.Round(ALeft, AMidpointStrategy);
            }
            public override decimal Frac(decimal ALeft)
            {
                return ALeft - this.Trunc(ALeft);
            }
            public override decimal Divide(decimal ALeft, decimal ARight)
            {
                return ALeft / ARight;
            }

            public override decimal Infinity { get { return decimal.MaxValue; } }
            public override decimal NaN { get { return decimal.MaxValue; } }

            public override NumeralType Type { get { return NumeralType.Fractional; } }
        }

        public abstract TNumeral Invert(TNumeral ALeft);
        public abstract TNumeral Ceil(TNumeral ALeft);
        public abstract TNumeral Trunc(TNumeral ALeft);
        public abstract TNumeral Round(TNumeral ALeft, MidpointRounding AMidpointStrategy);
        public abstract TNumeral Frac(TNumeral ALeft);

        public abstract TNumeral Divide(TNumeral ALeft, TNumeral ARight);

        public abstract TNumeral Infinity { get; }
        public abstract TNumeral NaN { get; }
    }
}