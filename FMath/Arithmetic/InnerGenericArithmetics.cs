using System;

namespace FMath.Arithmetic
{
    public static class InnerGenericArithmetics
    {
        public static NumeralType Type<TNumeral>()
        {
            return ArithmeticProvider<TNumeral>.Instance.Type;
        }

        public static TNumeral Add<TNumeral>(TNumeral ALeft, TNumeral ARight)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsNatural().Add(ALeft, ARight);
        }
        public static TNumeral Multiply<TNumeral>(TNumeral ALeft, TNumeral ARight)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsNatural().Multiply(ALeft, ARight);
        }
        public static TNumeral Div<TNumeral>(TNumeral ALeft, TNumeral ARight)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsNatural().Div(ALeft, ARight);
        }
        public static TNumeral Mod<TNumeral>(TNumeral ALeft, TNumeral ARight)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsNatural().Mod(ALeft, ARight);
        }

        public static bool IsModular<TNumeral>()
        {
            return ArithmeticProvider<TNumeral>.Instance.AsNatural().IsModular;
        }
        public static TNumeral Zero<TNumeral>()
        {
            return ArithmeticProvider<TNumeral>.Instance.AsNatural().Zero;  
        }
        public static TNumeral One<TNumeral>()
        {
            return ArithmeticProvider<TNumeral>.Instance.AsNatural().One; 
        }

        public static TNumeral Sign<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsInteger().Sign(ALeft);
        }
        public static TNumeral Absolute<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsInteger().Absolute(ALeft);
        }
        public static TNumeral Negate<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsInteger().Negate(ALeft);
        }

        public static TNumeral Invert<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsFractional().Invert(ALeft);
        }
        public static TNumeral Ceil<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsFractional().Ceil(ALeft);
        }
        public static TNumeral Trunc<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsFractional().Trunc(ALeft);
        }
        public static TNumeral Round<TNumeral>(TNumeral ALeft, MidpointRounding AMidpointStrategy)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsFractional().Round(ALeft, AMidpointStrategy);
        }
        public static TNumeral Frac<TNumeral>(TNumeral ALeft)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsFractional().Frac(ALeft);
        }
        public static TNumeral Divide<TNumeral>(TNumeral ALeft, TNumeral ARight)
        {
            return ArithmeticProvider<TNumeral>.Instance.AsFractional().Divide(ALeft, ARight);
        }

        public static TNumeral Infinity<TNumeral>()
        {
            return ArithmeticProvider<TNumeral>.Instance.AsFractional().Infinity; 
        }
        public static TNumeral NaN<TNumeral>()
        {
            return ArithmeticProvider<TNumeral>.Instance.AsFractional().NaN; 
        }
    }
}
