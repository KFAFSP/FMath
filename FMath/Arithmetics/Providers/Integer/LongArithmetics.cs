using System;

namespace FMath.Arithmetics.Providers.Integer
{
    public sealed class LongArithmetics :
        IntegerArithmeticProvider<long>
    {
        public override long Add(long ALeft, long ARight)
        {
            return unchecked(ALeft + ARight) % long.MaxValue;
        }
        public override long Multiply(long ALeft, long ARight)
        {
            return unchecked(ALeft * ARight) % long.MaxValue;
        }

        public override long IntDivision(long ALeft, long ARight, out long ARest)
        {
            ARest = ALeft % ARight;
            return ALeft / ARight;
        }

        public override long Sign(long ALeft)
        {
            return Math.Sign(ALeft);
        }

        public override long Negate(long ALeft)
        {
            return unchecked(-ALeft) % long.MaxValue;
        }

        public override long Zero { get { return 0; } }
        public override long One { get { return 1; } }

        public override NumeralType NumberType { get { return NumeralType.Integer; } }
    }
}