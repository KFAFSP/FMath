namespace FMath.Linear.Generic
{
    public delegate IVector<TData> ImmutableVectorFactory<TData>(TData[] AElements);

    public delegate IMutableVector<TData> MutableVectorFactory<TData>(int ASize);

    public delegate IMatrix<TData> ImmutableMatrixFactory<TData>(TData[,] ACells);

    public delegate IMutableMatrix<TData> MutableMatrixFactory<TData>(MatrixIndices ASize);
}
