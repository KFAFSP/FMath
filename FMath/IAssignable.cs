namespace FMath
{
    public interface IAssignable<in TFrom>
    {
        void Assign(TFrom AFrom);
    }
}
