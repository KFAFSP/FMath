namespace FMath.Linear.Generic
{
    public interface IMutableMatrix<TData> :
        IMutableMatrix,
        IMatrix<TData>
    {
        void Set(MatrixIndices AIndices, TData AValue);

        new TData this[int ARow, int ACol] { get; set; }
    }
}