using System;

namespace FMath.Arithmetics
{
    public static class GenericArithmetics
    {
        public static TType Add<TType>(TType ALeft, TType ARight)
        {
            ArithmeticProvider<TType> apProvider = ArithmeticProvider<TType>.Instance;
            if (!ArithmeticProvider<TType>.IsNatural)
                throw new InvalidOperationException("Type is not natural.");

            return ((NaturalArithmeticProvider<TType>)apProvider).Add(ALeft, ARight);
        }
        public static TType Multiply<TType>(TType ALeft, TType ARight)
        {
            ArithmeticProvider<TType> apProvider = ArithmeticProvider<TType>.Instance;
            if (!ArithmeticProvider<TType>.IsNatural)
                throw new InvalidOperationException("Type is not natural.");

            return ((NaturalArithmeticProvider<TType>)apProvider).Multiply(ALeft, ARight);
        }

        public static TType Negate<TType>(TType ALeft)
        {
            ArithmeticProvider<TType> apProvider = ArithmeticProvider<TType>.Instance;
            if (!ArithmeticProvider<TType>.IsInteger)
                throw new InvalidOperationException("Type is not integer.");

            return ((IntegerArithmeticProvider<TType>)apProvider).Negate(ALeft);
        }
        public static TType Subtract<TType>(TType ALeft, TType ARight)
        {
            ArithmeticProvider<TType> apProvider = ArithmeticProvider<TType>.Instance;
            if (!ArithmeticProvider<TType>.IsInteger)
                throw new InvalidOperationException("Type is not integer.");

            return ((IntegerArithmeticProvider<TType>)apProvider).Subtract(ALeft, ARight);
        }

        public static TType Invert<TType>(TType ALeft)
        {
            ArithmeticProvider<TType> apProvider = ArithmeticProvider<TType>.Instance;
            if (!ArithmeticProvider<TType>.IsReal)
                throw new InvalidOperationException("Type is not real.");

            return ((RealArithmeticProvider<TType>)apProvider).Invert(ALeft);
        }
        public static TType Divide<TType>(TType ALeft, TType ARight)
        {
            ArithmeticProvider<TType> apProvider = ArithmeticProvider<TType>.Instance;
            if (!ArithmeticProvider<TType>.IsReal)
                throw new InvalidOperationException("Type is not real.");

            return ((RealArithmeticProvider<TType>)apProvider).Divide(ALeft, ARight);
        }

        public static TType GetZero<TType>()
        {
            ArithmeticProvider<TType> apProvider = ArithmeticProvider<TType>.Instance;
            if (!ArithmeticProvider<TType>.IsNatural)
                throw new InvalidOperationException("Type is not natural.");

            return ((NaturalArithmeticProvider<TType>)apProvider).Zero;
        }
        public static TType GetOne<TType>()
        {
            ArithmeticProvider<TType> apProvider = ArithmeticProvider<TType>.Instance;
            if (!ArithmeticProvider<TType>.IsNatural)
                throw new InvalidOperationException("Type is not natural.");

            return ((NaturalArithmeticProvider<TType>)apProvider).One;
        }
    }
}
