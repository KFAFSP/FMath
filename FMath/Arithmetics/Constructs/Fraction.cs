using System;

namespace FMath.Arithmetics.Constructs
{
    public struct Fraction<TNumeral>
    {
        public readonly TNumeral Numerator;
        public readonly TNumeral Denominator;

        public Fraction(TNumeral ANumerator, TNumeral ADenominator)
        {
            if (ADenominator.Equals(GenericArithmetics.Zero<TNumeral>()))
                throw new ArgumentException("Denominator may not be zero.");

            TNumeral nGcd = GenericArithmetics.Gcd(ANumerator, ADenominator);

            if (nGcd.Equals(GenericArithmetics.One<TNumeral>()))
            {
                this.Numerator = ANumerator;
                this.Denominator = ADenominator;
            }
            else
            {

                this.Numerator = GenericArithmetics.Divide(ANumerator, nGcd);
                this.Denominator = GenericArithmetics.Divide(ADenominator, nGcd);
            }
        }
    }
}
