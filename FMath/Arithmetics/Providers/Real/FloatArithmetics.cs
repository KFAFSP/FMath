using System;

namespace FMath.Arithmetics.Providers.Real
{
    public sealed class FloatArithmetics :
        RealArithmeticProvider<float>
    {
        public override float Add(float ALeft, float ARight)
        {
            return ALeft + ARight;
        }
        public override float Multiply(float ALeft, float ARight)
        {
            return ALeft * ARight;
        }

        public override float IntDivision(float ALeft, float ARight, out float ARest)
        {
            ARest = ALeft%ARight;
            return ALeft/ARight;
        }

        public override float Negate(float ALeft)
        {
            return -ALeft;
        }

        public override float Round(float ALeft, RoundingMode AMode = RoundingMode.Down)
        {
            switch (AMode)
            {
                case RoundingMode.Down:
                    return (float)Math.Floor(ALeft);
                case RoundingMode.Up:
                    return (float)Math.Ceiling(ALeft);
                case RoundingMode.ClosestUp:
                    return (float)Math.Round(ALeft, MidpointRounding.AwayFromZero);
                case RoundingMode.ClosestDown:
                    return (float)Math.Round(ALeft, MidpointRounding.ToEven);
                default:
                    throw new ArgumentException("Unknown rounding mode.");
            }
        }

        public override float Invert(float ALeft)
        {
            return 1.0f/ALeft;
        }
        public override float Divide(float ALeft, float ARight)
        {
            return ALeft/ARight;
        }

        public override float Zero
        {
            get { return 0.0f; }
        }
        public override float One
        {
            get { return 1.0f; }
        }
    }
}
