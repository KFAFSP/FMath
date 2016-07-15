namespace FMath.Arithmetics.Providers.Real
{
    public sealed class DecimalArithmetics :
        RealArithmeticProvider<decimal>
    {
        public override decimal Add(decimal ALeft, decimal ARight)
        {
            return ALeft + ARight;
        }
        public override decimal Multiply(decimal ALeft, decimal ARight)
        {
            return ALeft * ARight;
        }
        public override decimal Negate(decimal ALeft)
        {
            return -ALeft;
        }
        public override decimal Invert(decimal ALeft)
        {
            return 1.0m / ALeft;
        }
        public override decimal Divide(decimal ALeft, decimal ARight)
        {
            return ALeft / ARight;
        }

        public override decimal Zero
        {
            get { return 0.0m; }
        }
        public override decimal One
        {
            get { return 1.0m; }
        }
    }
}