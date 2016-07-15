using System.Collections.Generic;

namespace FMath.Linear.Generic
{
    public interface IVector<out TData> :
        IVector,
        IEnumerable<TData>
    {
        new TData Get(int AIndex);

        new TData this[int AIndex] { get; }
    }
}