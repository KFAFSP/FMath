using System;

using FMath.Arithmetics;
using FMath.Linear.Generic;

namespace FMath.Linear.Static
{
    public static class NumericVector
    {
        public static bool IsNumeric<TData>(this IVector<TData> AVector)
        {
            return GenericArithmetics.IsNumeric<TData>();
        }

        public static void Negate<TData>(
            IVector<TData> ALeft,
            IMutableVector<TData> AOutput)
        {
            Vector.Map(ALeft, AOutput, GenericArithmetics.Negate);
        }
        public static void Add<TData>(
            IVector<TData> ALeft,
            IVector<TData> ARight,
            IMutableVector<TData> AOutput)
        {
            Vector.Combine(ALeft, ARight, AOutput, GenericArithmetics.Add);
        }
        public static void Subtract<TData>(
            IVector<TData> ALeft,
            IVector<TData> ARight,
            IMutableVector<TData> AOutput)
        {
            Vector.Combine(ALeft, ARight, AOutput, GenericArithmetics.Subtract);
        }
        public static void Multiply<TData>(
            IVector<TData> ALeft,
            TData ARight,
            IMutableVector<TData> AOutput)
        {
            Vector.Map(ALeft, AOutput, AElement => GenericArithmetics.Multiply(AElement, ARight));
        }
        public static void Divide<TData>(
            IVector<TData> ALeft,
            TData ARight,
            IMutableVector<TData> AOutput)
        {
            Vector.Map(ALeft, AOutput, AElement => GenericArithmetics.Divide(AElement, ARight));
        }

        public static TData EuclideanScalarProduct<TData>(
            IVector<TData> ALeft,
            IVector<TData> ARight)
        {
            if (ALeft == null)
                throw new ArgumentNullException("ALeft");
            if (ARight == null)
                throw new ArgumentNullException("ARight");

            if (ALeft.Size != ARight.Size)
                throw new ArgumentException("Vectors sizes do not match.");

            TData tResult = GenericArithmetics.GetZero<TData>();

            for (int I = 0; I < ALeft.Size; I++)
                tResult = GenericArithmetics.Add(tResult, GenericArithmetics.Multiply(ALeft.Get(I), ARight.Get(I)));

            return tResult;
        }
    }
}
