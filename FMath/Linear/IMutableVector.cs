namespace FMath.Linear
{
    public interface IMutableVector :
        IVector,
        IAssignable<IVector>
    {
        void Set(int AIndex, object AValue);
        new object this[int AIndex] { get; set; }
    }
}