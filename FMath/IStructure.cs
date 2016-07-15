using System;

namespace FMath
{
    /// <summary>
    /// Interface for types that are structures consisting of multiple elements.
    /// </summary>
    public interface IStructure
    {
        /// <summary>
        /// Gets the type of a structure element.
        /// </summary>
        /// <value>
        /// The type of a structure element.
        /// </value>
        Type ElementType { get; }
    }
}
