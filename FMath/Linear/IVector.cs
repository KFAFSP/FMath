using System;
using System.Collections;

namespace FMath.Linear
{
    public interface IVector :
        IStructure,
        ICloneable,
        IEnumerable
    {
        object Get(int AIndex);

        int Size { get; }

        object this[int AIndex] { get; }
    }
}
