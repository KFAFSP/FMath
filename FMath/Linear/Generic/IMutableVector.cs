namespace FMath.Linear.Generic
{
    public interface IMutableVector<TData> :
        IMutableVector,
        IVector<TData>
    {
        void Set(int AIndex, TData AValue);

        new TData this[int AIndex] { get; set; }
    }
}