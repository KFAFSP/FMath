namespace FMath.Linear.Generic
{
    public interface IMatrix<out TData> :
        IMatrix
    {
        new TData Get(MatrixIndices AIndices);

        new TData this[int ARow, int ACol] { get; }
    }
}
