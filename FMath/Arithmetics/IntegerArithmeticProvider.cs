using System.Diagnostics.Contracts;

namespace FMath.Arithmetics
{
    /// <summary>
    /// Abstract base class for integer arithemtic providers.
    /// </summary>
    /// <typeparam name="TType">The type that is provided for.</typeparam>
    /// <seealso cref="FMath.Arithmetics.NaturalArithmeticProvider{TType}" />
    public abstract class IntegerArithmeticProvider<TType> :
        NaturalArithmeticProvider<TType>
    {
        /// <summary>
        /// Returns a negated copy of the specified argument.
        /// </summary>
        /// <param name="ALeft">A value.</param>
        /// <returns>A negated copy.</returns>
        [Pure]
        public abstract TType Negate(TType ALeft);
        /// <summary>
        /// Returns the difference between the specified arguments.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <returns>The difference.</returns>
        [Pure]
        public virtual TType Subtract(TType ALeft, TType ARight)
        {
            return this.Add(ALeft, this.Negate(ARight));
        }
    }
}