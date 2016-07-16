using System;

namespace FMath.Arithmetics.Providers.Real
{
    public sealed class DecimalArithmetics :
        RealArithmeticProvider<decimal>
    {
        public override decimal Add(decimal ALeft, decimal ARight)
        {
            return ALeft + ARight;
        }
        public override decimal Multiply(decimal ALeft, decimal ARight)
        {
            return ALeft * ARight;
        }

        public override decimal IntDivision(decimal ALeft, decimal ARight, out decimal ARest)
        {
            ARest = ALeft % ARight;
            return ALeft / ARight;
        }

        public override decimal Sign(decimal ALeft)
        {
            return Math.Sign(ALeft);
        }

        public override decimal Negate(decimal ALeft)
        {
            return -ALeft;
        }

        public override decimal Round(decimal ALeft, RoundingMode AMode = RoundingMode.Down)
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

        public override decimal Invert(decimal ALeft)
        {
            return 1.0m / ALeft;
        }
        public override decimal Divide(decimal ALeft, decimal ARight)
        {
            return ALeft / ARight;
        }

        public override decimal Zero
        {
            get { return 0.0m; }
        }
        public override decimal One
        {
            get { return 1.0m; }
        }

        public override NumeralType NumberType { get { return NumeralType.Real; } }
    }
}