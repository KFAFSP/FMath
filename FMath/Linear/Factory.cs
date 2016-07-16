namespace FMath.Linear
{
    public delegate IVector ImmutableVectorFactory(object[] AElements);

    public delegate IMutableVector MutableVectorFactory(int ASize);

    public delegate IMatrix ImmutableMatrixFactory(object[,] ACells);

    public delegate IMutableMatrix MutableMatrixFactory(MatrixIndices ASize);
}
