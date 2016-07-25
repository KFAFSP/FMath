namespace FMath.Linear
{
    public delegate IVector ImmutableVectorFactoryDelegate(object[] AElements);

    public delegate IMutableVector MutableVectorFactoryDelegate(int ASize);

    public delegate IMatrix ImmutableMatrixFactoryDelegate(object[,] ACells);

    public delegate IMutableMatrix MutableMatrixFactoryDelegate(MatrixIndices ASize);
}
