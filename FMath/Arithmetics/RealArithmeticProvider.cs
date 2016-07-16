using System.Diagnostics.Contracts;

namespace FMath.Arithmetics
{
    /// <summary>
    /// Abstract base class for real arithemtic providers.
    /// </summary>
    /// <typeparam name="TNumeral">The type that is provided for.</typeparam>
    /// <seealso cref="FMath.Arithmetics.IntegerArithmeticProvider{TType}" />
    public abstract class RealArithmeticProvider<TNumeral> :
        IntegerArithmeticProvider<TNumeral>
    {
        /// <summary>
        /// Rounds a real number to the specified integer representation.
        /// </summary>
        /// <param name="ALeft">The value.</param>
        /// <param name="AMode">The rounding mode, defaults to always rounding down.</param>
        /// <returns>The integer representation.</returns>
        public abstract TNumeral Round(TNumeral ALeft, RoundingMode AMode = RoundingMode.Down);

        /// <summary>
        /// Returns an inverted copy of the specified argument.
        /// </summary>
        /// <param name="ALeft">The value.</param>
        /// <returns>The inverted copy.</returns>
        [Pure]
        public abstract TNumeral Invert(TNumeral ALeft);
        /// <summary>
        /// Divides the first through the second argument.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <returns>The quotient.</returns>
        [Pure]
        public virtual TNumeral Divide(TNumeral ALeft, TNumeral ARight)
        {
            return this.Multiply(ALeft, this.Invert(ARight));
        }
    }
}