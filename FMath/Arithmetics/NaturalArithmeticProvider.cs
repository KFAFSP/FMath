using System.Diagnostics.Contracts;

namespace FMath.Arithmetics
{
    /// <summary>
    /// Abstract base class for natural arithemtic providers.
    /// </summary>
    /// <typeparam name="TNumeral">The type that is provided for.</typeparam>
    /// <seealso cref="FMath.Arithmetics.ArithmeticProvider{TType}" />
    public abstract class NaturalArithmeticProvider<TNumeral> :
        ArithmeticProvider<TNumeral>
    {
        /// <summary>
        /// Returns the sum of two specified arguments.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <returns>The sum.</returns>
        [Pure]
        public abstract TNumeral Add(TNumeral ALeft, TNumeral ARight);
        /// <summary>
        /// Returns the product of two specified arguments.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <returns>The product.</returns>
        [Pure]
        public abstract TNumeral Multiply(TNumeral ALeft, TNumeral ARight);

        /// <summary>
        /// Performs an integer division of the two specified arguments.
        /// </summary>
        /// <param name="ALeft">The left hand side.</param>
        /// <param name="ARight">The right hand side.</param>
        /// <param name="ARest">The division rest.</param>
        /// <returns>The integer part of the quotient.</returns>
        [Pure]
        public abstract TNumeral IntDivision(TNumeral ALeft, TNumeral ARight, out TNumeral ARest);

        /// <summary>
        /// Gets the neutral element of the addition.
        /// </summary>
        /// <value>
        /// The neutral element of the addition.
        /// </value>
        public abstract TNumeral Zero { [Pure] get; }
        /// <summary>
        /// Gets the neutral element of the multiplication.
        /// </summary>
        /// <value>
        /// The neutral element of the multiplication.
        /// </value>
        public abstract TNumeral One { [Pure] get; }
    }
}
