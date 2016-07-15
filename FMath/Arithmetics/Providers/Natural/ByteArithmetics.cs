namespace FMath.Arithmetics.Providers.Natural
{
    public sealed class ByteArithmetics :
        NaturalArithmeticProvider<byte>
    {
        public override byte Add(byte ALeft, byte ARight)
        {
            return (byte)(unchecked (ALeft + ARight) % byte.MaxValue);
        }
        public override byte Multiply(byte ALeft, byte ARight)
        {
            return (byte)(unchecked (ALeft * ARight) % byte.MaxValue);
        }

        public override byte Zero { get { return 0; } }
        public override byte One { get { return 1; } }
    }
}
