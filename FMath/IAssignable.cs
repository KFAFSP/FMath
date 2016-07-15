namespace FMath
{
    /// <summary>
    /// Interface for types that provide a method that copies data from another instance to this instance.
    /// </summary>
    /// <typeparam name="TFrom">The type that can be copied from.</typeparam>
    public interface IAssignable<in TFrom>
    {
        /// <summary>
        /// Copies data from the input object to this object.
        /// </summary>
        /// <param name="AFrom">The input object to copy from.</param>
        void Assign(TFrom AFrom);
    }
}
