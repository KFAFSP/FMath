namespace FMath.Linear
{
    public interface IMutableMatrix :
        IMatrix,
        IAssignable<IMatrix>
    {
        void Set(MatrixIndices AIndices, object AValue);
        new object this[int ARow, int ACol] { get; set; }
    }
}