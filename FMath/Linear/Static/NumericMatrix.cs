using FMath.Arithmetics;
using FMath.Linear.Generic;

namespace FMath.Linear.Static
{
    public static class NumericMatrix
    {
        public static bool IsNumeric<TData>(this IMatrix<TData> AVector)
        {
            return GenericArithmetics.IsNumeric<TData>();
        }

        public static void Negate<TData>(
            IMatrix<TData> ALeft,
            IMutableMatrix<TData> AOutput)
        {
            Matrix.Map(ALeft, AOutput, GenericArithmetics.Negate);
        }
        public static void Add<TData>(
            IMatrix<TData> ALeft,
            IMatrix<TData> ARight,
            IMutableMatrix<TData> AOutput)
        {
            Matrix.Combine(ALeft, ARight, AOutput, GenericArithmetics.Add);
        }
        public static void Subtract<TData>(
            IMatrix<TData> ALeft,
            IMatrix<TData> ARight,
            IMutableMatrix<TData> AOutput)
        {
            Matrix.Combine(ALeft, ARight, AOutput, GenericArithmetics.Subtract);
        }
        public static void Multiply<TData>(
            IMatrix<TData> ALeft,
            TData ARight,
            IMutableMatrix<TData> AOutput)
        {
            Matrix.Map(ALeft, AOutput, AElement => GenericArithmetics.Multiply(AElement, ARight));
        }
        public static void Divide<TData>(
            IMatrix<TData> ALeft,
            TData ARight,
            IMutableMatrix<TData> AOutput)
        {
            Matrix.Map(ALeft, AOutput, AElement => GenericArithmetics.Divide(AElement, ARight));
        }

        public static void Multiply<TData>(
            IMatrix<TData> ALeft,
            IMatrix<TData> ARight,
            IMutableMatrix<TData> AOutput)
        {
            Matrix.Multiply(ALeft, ARight, AOutput, GenericArithmetics.Multiply, GenericArithmetics.Add, GenericArithmetics.GetZero<TData>());
        }
    }
}