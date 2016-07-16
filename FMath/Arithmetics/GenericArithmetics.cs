namespace FMath.Arithmetics
{
    public static class GenericArithmetics
    {
        public static TNumeral Add<TNumeral>(TNumeral ALeft, TNumeral ARight)
        {
            return ArithmeticProvider<TNumeral>.AsNatural.Add(ALeft, ARight);
        }
        public static TNumeral Multiply<TNumeral>(TNumeral ALeft, TNumeral ARight)
        {
            return ArithmeticProvider<TNumeral>.AsNatural.Multiply(ALeft, ARight);
        }

        public static TNumeral IntDivision<TNumeral>(TNumeral ALeft, TNumeral ARight, out TNumeral ARest)
        {
            return ArithmeticProvider<TNumeral>.AsNatural.IntDivision(ALeft, ARight, out ARest);
        }

        public static TNumeral Zero<TNumeral>()
        {
            return ArithmeticProvider<TNumeral>.AsNatural.Zero;
        }
        public static TNumeral One<TNumeral>()
        {
            return ArithmeticProvider<TNumeral>.AsNatural.One;
        }

        public static TNumeral Sign<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.AsInteger.Sign(ALeft);
        }

        public static TNumeral NegativeOne<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.AsInteger.NegativeOne;
        }

        public static TNumeral Absolute<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.AsInteger.Absolute(ALeft);
        }
        public static TNumeral Negate<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.AsInteger.Negate(ALeft);
        }
        public static TNumeral Subtract<TNumeral>(TNumeral ALeft, TNumeral ARight)
        {
            return ArithmeticProvider<TNumeral>.AsInteger.Subtract(ALeft, ARight);
        }

        public static TNumeral Round<TNumeral>(TNumeral ALeft, RoundingMode AMode = RoundingMode.Down)
        {
            return ArithmeticProvider<TNumeral>.AsReal.Round(ALeft, AMode);
        }

        public static TNumeral Invert<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.AsReal.Invert(ALeft);
        }
        public static TNumeral Divide<TNumeral>(TNumeral ALeft, TNumeral ARight)
        {
            return ArithmeticProvider<TNumeral>.AsReal.Divide(ALeft, ARight);
        }

        public static TNumeral Gcd<TNumeral>(TNumeral ALeft, TNumeral ARight)
        {
            if (ARight.Equals(GenericArithmetics.Zero<TNumeral>()))
                return ALeft;

            TNumeral nRest;
            GenericArithmetics.IntDivision(ALeft, ARight, out nRest);

            return GenericArithmetics.Gcd<TNumeral>(ARight, nRest);
        }
        public static TNumeral Lcm<TNumeral>(TNumeral ALeft, TNumeral ARight)
        {
            TNumeral nGcd = GenericArithmetics.Gcd(ALeft, ARight);
            return GenericArithmetics.Multiply(GenericArithmetics.Divide(GenericArithmetics.Absolute(ALeft), nGcd), GenericArithmetics.Divide(GenericArithmetics.Absolute(ARight), nGcd));
        }
    }
}