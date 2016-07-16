using System.Diagnostics.Contracts;

namespace FMath.Arithmetics
{
    public abstract class NaturalArithmeticProvider<TNumeral> :
        ArithmeticProvider<TNumeral>
    {
        [Pure]
        public abstract TNumeral Add(TNumeral ALeft, TNumeral ARight);
        [Pure]
        public abstract TNumeral Multiply(TNumeral ALeft, TNumeral ARight);
        [Pure]
        public abstract TNumeral IntDivision(TNumeral ALeft, TNumeral ARight, out TNumeral ARest);
        public abstract TNumeral Zero { [Pure] get; }
        public abstract TNumeral One { [Pure] get; }
    }
}
