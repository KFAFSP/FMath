namespace FMath.Arithmetics.Providers.Natural
{
    public sealed class UIntArithmetics :
        NaturalArithmeticProvider<uint>
    {
        public override uint Add(uint ALeft, uint ARight)
        {
            return unchecked (ALeft + ARight) % uint.MaxValue;
        }
        public override uint Multiply(uint ALeft, uint ARight)
        {
            return unchecked (ALeft * ARight) % uint.MaxValue;
        }

        public override uint Zero { get { return 0; } }
        public override uint One { get { return 1; } }
    }
}