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

        public static void Negate<TNumeral>(
            IMatrix<TNumeral> AIn,
            IMutableMatrix<TNumeral> AOut)
        {
            Matrix.Map(AIn, AOut, InnerGenericArithmetics.Negate);
        }
        public static void Add<TNumeral>(
            IMatrix<TNumeral> ALeft,
            IMatrix<TNumeral> ARight,
            IMutableMatrix<TNumeral> AOut)
        {
            Matrix.Combine(ALeft, ARight, AOut, InnerGenericArithmetics.Add);
        }
        public static void Subtract<TNumeral>(
            IMatrix<TNumeral> ALeft,
            IMatrix<TNumeral> ARight,
            IMutableMatrix<TNumeral> AOut)
        {
            Matrix.Combine(ALeft, ARight, AOut, (AL, AR) => InnerGenericArithmetics.Add(AL, InnerGenericArithmetics.Negate(AR)));
        }
        public static void Multiply<TNumeral>(
            IMatrix<TNumeral> ALeft,
            TNumeral ARight,
            IMutableMatrix<TNumeral> AOut)
        {
            Matrix.Map(ALeft, AOut, AL => InnerGenericArithmetics.Multiply(AL, ARight));
        }
        public static void Divide<TNumeral>(
            IMatrix<TNumeral> ALeft,
            TNumeral ARight,
            IMutableMatrix<TNumeral> AOut)
        {
            Matrix.Map(ALeft, AOut, AL => InnerGenericArithmetics.Divide(AL, ARight));
        }
        public static void Multiply<TNumeral>(
            IMatrix<TNumeral> ALeft,
            IMatrix<TNumeral> ARight,
            IMutableMatrix<TNumeral> AOut)
        {
            Matrix.Multiply(ALeft, ARight, AOut, InnerGenericArithmetics.Multiply, InnerGenericArithmetics.Add, InnerGenericArithmetics.Zero<TNumeral>());
        }

        public static void Trunc<TNumeral>(
            IMatrix<TNumeral> AIn,
            IMutableMatrix<TNumeral> AOut)
        {
            Matrix.Map(AIn, AOut, InnerGenericArithmetics.Trunc);
        }
    }
}
