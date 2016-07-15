namespace FMath.Arithmetics
{
    public abstract class RealArithmeticProvider<TType> :
        IntegerArithmeticProvider<TType>
    {
        public abstract TType Invert(TType ALeft);
        public virtual TType Divide(TType ALeft, TType ARight)
        {
            return this.Multiply(ALeft, this.Invert(ARight));
        }
    }
}