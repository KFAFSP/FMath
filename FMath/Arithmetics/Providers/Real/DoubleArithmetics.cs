namespace FMath.Arithmetics.Providers.Real
{
    public sealed class DoubleArithmetics :
        RealArithmeticProvider<double>
    {
        public override double Add(double ALeft, double ARight)
        {
            return ALeft + ARight;
        }
        public override double Multiply(double ALeft, double ARight)
        {
            return ALeft * ARight;
        }
        public override double Negate(double ALeft)
        {
            return -ALeft;
        }
        public override double Invert(double ALeft)
        {
            return 1.0d / ALeft;
        }
        public override double Divide(double ALeft, double ARight)
        {
            return ALeft / ARight;
        }

        public override double Zero
        {
            get { return 0.0d; }
        }
        public override double One
        {
            get { return 1.0d; }
        }
    }
}