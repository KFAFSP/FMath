using System;

namespace FMath.Linear
{
    public interface IMatrix :
        IStructure,
        ICloneable
    {
        object Get(MatrixIndices AIndices);

        MatrixIndices Size { get; }

        object this[int ARow, int ACol] { get; }
    }
}