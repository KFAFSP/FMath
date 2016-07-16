using System;

namespace FMath.Arithmetics.Providers.Real
{
    public sealed class DoubleArithmetics :
        RealArithmeticProvider<double>
    {
        public override double Add(double ALeft, double ARight)
        {
            return ALeft + ARight;
        }
        public override double Multiply(double ALeft, double ARight)
        {
            return ALeft * ARight;
        }

        public override double IntDivision(double ALeft, double ARight, out double ARest)
        {
            ARest = ALeft % ARight;
            return ALeft / ARight;
        }

        public override double Sign(double ALeft)
        {
            return Math.Sign(ALeft);
        }

        public override double Negate(double ALeft)
        {
            return -ALeft;
        }

        public override double Round(double ALeft, RoundingMode AMode = RoundingMode.Down)
        {
            switch (AMode)
            {
                case RoundingMode.Down:
                    return Math.Floor(ALeft);
                case RoundingMode.Up:
                    return Math.Ceiling(ALeft);
                case RoundingMode.ClosestUp:
                    return Math.Round(ALeft, MidpointRounding.AwayFromZero);
                case RoundingMode.ClosestDown:
                    return Math.Round(ALeft, MidpointRounding.ToEven);
                default:
                    throw new ArgumentException("Unknown rounding mode.");
            }
        }

        public override double Invert(double ALeft)
        {
            return 1.0d / ALeft;
        }
        public override double Divide(double ALeft, double ARight)
        {
            return ALeft / ARight;
        }

        public override double Zero
        {
            get { return 0.0d; }
        }
        public override double One
        {
            get { return 1.0d; }
        }

        public override NumeralType NumberType { get { return NumeralType.Real; } }
    }
}