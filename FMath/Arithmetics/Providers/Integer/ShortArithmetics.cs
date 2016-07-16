namespace FMath.Arithmetics.Providers.Integer
{
    public sealed class ShortArithmetics :
        IntegerArithmeticProvider<short>
    {
        public override short Add(short ALeft, short ARight)
        {
            return (short)(unchecked(ALeft + ARight) % short.MaxValue);
        }
        public override short Multiply(short ALeft, short ARight)
        {
            return (short)(unchecked(ALeft * ARight) % short.MaxValue);
        }

        public override short IntDivision(short ALeft, short ARight, out short ARest)
        {
            ARest = (short)(ALeft % ARight);
            return (short)(ALeft / ARight);
        }

        public override short Negate(short ALeft)
        {
            return (short)(unchecked(-ALeft) % short.MaxValue);
        }

        public override short Zero { get { return 0; } }
        public override short One { get { return 1; } }
    }
}
