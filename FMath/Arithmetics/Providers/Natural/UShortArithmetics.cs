namespace FMath.Arithmetics.Providers.Natural
{
    public sealed class UShortArithmetics :
        NaturalArithmeticProvider<ushort>
    {
        public override ushort Add(ushort ALeft, ushort ARight)
        {
            return (ushort)(unchecked (ALeft + ARight) % ushort.MaxValue);
        }
        public override ushort Multiply(ushort ALeft, ushort ARight)
        {
            return (ushort)(unchecked (ALeft * ARight) % ushort.MaxValue);
        }

        public override ushort IntDivision(ushort ALeft, ushort ARight, out ushort ARest)
        {
            ARest = (ushort)(ALeft % ARight);
            return (ushort)(ALeft / ARight);
        }

        public override ushort Zero { get { return 0; } }
        public override ushort One { get { return 1; } }

        public override NumeralType NumberType { get { return NumeralType.Natural; } }
    }
}