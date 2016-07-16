namespace FMath.Arithmetics
{
    /// <summary>
    /// Enumeration that represents different rounding strategies.
    /// </summary>
    public enum RoundingMode
    {
        /// <summary>
        /// Always round down.
        /// </summary>
        Down = 0,
        /// <summary>
        /// Always round up.
        /// </summary>
        Up = 1,
        /// <summary>
        /// Round to the closest integer, prefer downwards.
        /// </summary>
        ClosestDown = 2,
        /// <summary>
        /// Round to the closest integer, prefer upwards.
        /// </summary>
        ClosestUp = 3,
    }
}