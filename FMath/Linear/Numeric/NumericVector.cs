using System;

using FMath.Arithmetic;
using FMath.Linear.Generic;
using FMath.Linear.Static;

namespace FMath.Linear.Numeric
{
    public static class NumericVector
    {
        public static bool IsNumeric<TData>(this IVector<TData> AVector)
        {
            if (AVector == null)
                throw new ArgumentNullException("AVector");

            return InnerGenericArithmetics.IsNumeral<TData>();
        }

        public static void Negate<TData>(
            IVector<TData> ASource,
            IMutableVector<TData> ATarget)
        {
            Vector.Map(ASource, ATarget, InnerGenericArithmetics.Negate);
        }
        public static void Add<TData>(
            IVector<TData> ALeft,
            IVector<TData> ARight,
            IMutableVector<TData> AOut)
        {
            Vector.Combine(ALeft, ARight, AOut, InnerGenericArithmetics.Add);
        }
        public static void Subtract<TData>(
            IVector<TData> ALeft,
            IVector<TData> ARight,
            IMutableVector<TData> AOut)
        {
            Vector.Combine(ALeft, ARight, AOut, (AL, AR) => InnerGenericArithmetics.Add(AL, InnerGenericArithmetics.Negate(AR)));
        }
        public static void Multiply<TData>(
            IVector<TData> ALeft,
            TData ARight,
            IMutableVector<TData> AOut)
        {
            Vector.Map(ALeft, AOut, AL => InnerGenericArithmetics.Multiply(AL, ARight));
        }
        public static void Divide<TData>(
            IVector<TData> ALeft,
            TData ARight,
            IMutableVector<TData> AOut)
        {
            Vector.Map(ALeft, AOut, AL => InnerGenericArithmetics.Divide(AL, ARight));
        }
    }
}
