namespace FMath.Arithmetics.Providers.Integer
{
    public sealed class IntArithmetics :
        IntegerArithmeticProvider<int>
    {
        public override int Add(int ALeft, int ARight)
        {
            return unchecked(ALeft + ARight) % int.MaxValue;
        }
        public override int Multiply(int ALeft, int ARight)
        {
            return unchecked(ALeft * ARight) % int.MaxValue;
        }
        public override int Negate(int ALeft)
        {
            return unchecked(-ALeft) % int.MaxValue;
        }

        public override int Zero { get { return 0; } }
        public override int One { get { return 1; } }
    }
}