namespace FMath.Arithmetics.Providers.Natural
{
    public sealed class ULongArithmetics :
        NaturalArithmeticProvider<ulong>
    {
        public override ulong Add(ulong ALeft, ulong ARight)
        {
            return unchecked (ALeft + ARight);
        }
        public override ulong Multiply(ulong ALeft, ulong ARight)
        {
            return unchecked (ALeft * ARight);
        }

        public override ulong IntDivision(ulong ALeft, ulong ARight, out ulong ARest)
        {
            ARest = ALeft % ARight;
            return ALeft / ARight;
        }

        public override ulong Zero { get { return 0; } }
        public override ulong One { get { return 1; } }
    }
}