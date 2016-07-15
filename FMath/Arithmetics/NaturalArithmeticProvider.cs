namespace FMath.Arithmetics
{
    public abstract class NaturalArithmeticProvider<TType> :
        ArithmeticProvider<TType>
    {
        public abstract TType Add(TType ALeft, TType ARight);
        public abstract TType Multiply(TType ALeft, TType ARight);

        public abstract TType Zero { get; }
        public abstract TType One { get; }
    }
}
