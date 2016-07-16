using System;

using FMath.Arithmetic;
using FMath.Linear.Generic;
using FMath.Linear.Static;

namespace FMath.Linear.Numeric
{
    public static class NumericMatrix
    {
        public static bool IsNumeric<TData>(this IMatrix<TData> AMatrix)
        {
            if (AMatrix == null)
                throw new ArgumentNullException("AMatrix");

            return InnerGenericArithmetics.IsNumeral<TData>();
        }

        public static void Negate<TData>(
            IMatrix<TData> ASource,
            IMutableMatrix<TData> ATarget)
        {
            Matrix.Map(ASource, ATarget, InnerGenericArithmetics.Negate);
        }
        public static void Add<TData>(
            IMatrix<TData> ALeft,
            IMatrix<TData> ARight,
            IMutableMatrix<TData> AOut)
        {
            Matrix.Combine(ALeft, ARight, AOut, InnerGenericArithmetics.Add);
        }
        public static void Subtract<TData>(
            IMatrix<TData> ALeft,
            IMatrix<TData> ARight,
            IMutableMatrix<TData> AOut)
        {
            Matrix.Combine(ALeft, ARight, AOut, (AL, AR) => InnerGenericArithmetics.Add(AL, InnerGenericArithmetics.Negate(AR)));
        }
        public static void Multiply<TData>(
            IMatrix<TData> ALeft,
            TData ARight,
            IMutableMatrix<TData> AOut)
        {
            Matrix.Map(ALeft, AOut, AL => InnerGenericArithmetics.Multiply(AL, ARight));
        }
        public static void Divide<TData>(
            IMatrix<TData> ALeft,
            TData ARight,
            IMutableMatrix<TData> AOut)
        {
            Matrix.Map(ALeft, AOut, AL => InnerGenericArithmetics.Divide(AL, ARight));
        }
        public static void Multiply<TData>(
            IMatrix<TData> ALeft,
            IMatrix<TData> ARight,
            IMutableMatrix<TData> AOut)
        {
            Matrix.Multiply(ALeft, ARight, AOut, InnerGenericArithmetics.Multiply, InnerGenericArithmetics.Add, InnerGenericArithmetics.Zero<TData>());
        }
    }
}
