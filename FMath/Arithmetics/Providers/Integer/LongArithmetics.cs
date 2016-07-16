﻿namespace FMath.Arithmetics.Providers.Integer
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
        public override long Negate(long ALeft)
        {
            return unchecked(-ALeft) % long.MaxValue;
        }

        public override long Zero { get { return 0; } }
        public override long One { get { return 1; } }
    }
}