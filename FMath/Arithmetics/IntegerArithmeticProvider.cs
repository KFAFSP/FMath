namespace FMath.Arithmetics
{
    public abstract class IntegerArithmeticProvider<TType> :
        NaturalArithmeticProvider<TType>
    {
        public abstract TType Negate(TType ALeft);
        public virtual TType Subtract(TType ALeft, TType ARight)
        {
            return this.Add(ALeft, this.Negate(ARight));
        }
    }
}