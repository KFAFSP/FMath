using System.Diagnostics.Contracts;

namespace FMath.Arithmetics
{
    public abstract class IntegerArithmeticProvider<TNumeral> :
        NaturalArithmeticProvider<TNumeral>
    {
        [Pure]
        public abstract TNumeral Negate(TNumeral ALeft);
        [Pure]
        public abstract TNumeral Sign(TNumeral ALeft);
        [Pure]
        public virtual TNumeral Absolute(TNumeral ALeft)
        {
            if (this.Sign(ALeft).Equals(this.NegativeOne))
                return this.Negate(ALeft);

            return ALeft;
        }
        [Pure]
        public virtual TNumeral Subtract(TNumeral ALeft, TNumeral ARight)
        {
            return this.Add(ALeft, this.Negate(ARight));
        }
        public virtual TNumeral NegativeOne { get { return this.Negate(this.One); } }
    }
}