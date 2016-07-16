using System.Diagnostics.Contracts;

namespace FMath.Arithmetics
{
    /// <summary>
    /// Abstract base class for integer arithemtic providers.
    /// </summary>
    /// <typeparam name="TNumeral">The type that is provided for.</typeparam>
    /// <seealso cref="FMath.Arithmetics.NaturalArithmeticProvider{TType}" />
    public abstract class IntegerArithmeticProvider<TNumeral> :
        NaturalArithmeticProvider<TNumeral>
    {
        /// <summary>
        /// Returns a negated copy of the specified argument.
        /// </summary>
        /// <param name="ALeft">A value.</param>
        /// <returns>A negated copy.</returns>
        [Pure]
        public abstract TNumeral Negate(TNumeral ALeft);
        /// <summary>
        /// Gets the sign of the argument.
        /// </summary>
        /// <param name="ALeft">A value.</param>
        /// <returns>Either <c>this.One</c>, <c>this.Zero</c> or <c>this.NegativeOne</c>.</returns>
        [Pure]
        public abstract TNumeral Sign(TNumeral ALeft);
        /// <summary>
        /// Gets the absolute value of the argument.
        /// </summary>
        /// <param name="ALeft">A value.</param>
        /// <returns>The absolute (non-negative) value.</returns>
        [Pure]
        public virtual TNumeral Absolute(TNumeral ALeft)
        {
            if (this.Sign(ALeft).Equals(this.NegativeOne))
                return this.Negate(ALeft);

            return ALeft;
        }
        /// <summary>
        /// Returns the difference between the specified arguments.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <returns>The difference.</returns>
        [Pure]
        public virtual TNumeral Subtract(TNumeral ALeft, TNumeral ARight)
        {
            return this.Add(ALeft, this.Negate(ARight));
        }

        /// <summary>
        /// Gets the negated one element.
        /// </summary>
        /// <value>
        /// The negated one element.
        /// </value>
        public virtual TNumeral NegativeOne { get { return this.Negate(this.One); } }
    }
}