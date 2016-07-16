namespace FMath.Arithmetics.Providers.Integer
{
    public sealed class SByteArithmetics :
        IntegerArithmeticProvider<sbyte>
    {
        public override sbyte Add(sbyte ALeft, sbyte ARight)
        {
            return (sbyte)(unchecked(ALeft + ARight) % sbyte.MaxValue);
        }
        public override sbyte Multiply(sbyte ALeft, sbyte ARight)
        {
            return (sbyte)(unchecked(ALeft * ARight) % sbyte.MaxValue);
        }

        public override sbyte IntDivision(sbyte ALeft, sbyte ARight, out sbyte ARest)
        {
            ARest = (sbyte)(ALeft % ARight);
            return (sbyte)(ALeft / ARight);
        }

        public override sbyte Negate(sbyte ALeft)
        {
            return (sbyte)(unchecked(-ALeft) % sbyte.MaxValue);
        }

        public override sbyte Zero { get { return 0; } }
        public override sbyte One { get { return 1; } }
    }
}