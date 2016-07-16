using System.Diagnostics.Contracts;

namespace FMath.Arithmetics
{
    public abstract class RealArithmeticProvider<TNumeral> :
        IntegerArithmeticProvider<TNumeral>
    {
        public abstract TNumeral Round(TNumeral ALeft, RoundingMode AMode = RoundingMode.Down);
        [Pure]
        public abstract TNumeral Invert(TNumeral ALeft);
        [Pure]
        public virtual TNumeral Divide(TNumeral ALeft, TNumeral ARight)
        {
            return this.Multiply(ALeft, this.Invert(ARight));
        }
    }
}