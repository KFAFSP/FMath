namespace FMath.Arithmetics.Providers.Real
{
    public sealed class FloatArithmetics :
        RealArithmeticProvider<float>
    {
        public override float Add(float ALeft, float ARight)
        {
            return ALeft + ARight;
        }
        public override float Multiply(float ALeft, float ARight)
        {
            return ALeft * ARight;
        }
        public override float Negate(float ALeft)
        {
            return -ALeft;
        }
        public override float Invert(float ALeft)
        {
            return 1.0f/ALeft;
        }
        public override float Divide(float ALeft, float ARight)
        {
            return ALeft/ARight;
        }

        public override float Zero
        {
            get { return 0.0f; }
        }
        public override float One
        {
            get { return 1.0f; }
        }
    }
}
