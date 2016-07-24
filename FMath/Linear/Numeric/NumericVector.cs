using System;

using FMath.Arithmetic;
using FMath.Linear.Generic;
using FMath.Linear.Generic.Immutable;
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

        public static ArrayVector<TNumeral> Zero<TNumeral>(int ASize)
        {
            return ArrayVector.Filled(ASize, InnerGenericArithmetics.Zero<TNumeral>());
        }
        public static ArrayVector<TNumeral> One<TNumeral>(int ASize)
        {
            return ArrayVector.Filled(ASize, InnerGenericArithmetics.One<TNumeral>());
        }

        public static void Negate<TNumeral>(
            IVector<TNumeral> AIn,
            IMutableVector<TNumeral> AOut)
        {
            Vector.Map(AIn, AOut, InnerGenericArithmetics.Negate);
        }
        public static void Add<TNumeral>(
            IVector<TNumeral> ALeft,
            IVector<TNumeral> ARight,
            IMutableVector<TNumeral> AOut)
        {
            Vector.Combine(ALeft, ARight, AOut, InnerGenericArithmetics.Add);
        }
        public static void Subtract<TNumeral>(
            IVector<TNumeral> ALeft,
            IVector<TNumeral> ARight,
            IMutableVector<TNumeral> AOut)
        {
            Vector.Combine(ALeft, ARight, AOut, (AL, AR) => InnerGenericArithmetics.Add(AL, InnerGenericArithmetics.Negate(AR)));
        }
        public static void Multiply<TNumeral>(
            IVector<TNumeral> ALeft,
            TNumeral ARight,
            IMutableVector<TNumeral> AOut)
        {
            Vector.Map(ALeft, AOut, AL => InnerGenericArithmetics.Multiply(AL, ARight));
        }
        public static void Divide<TNumeral>(
            IVector<TNumeral> ALeft,
            TNumeral ARight,
            IMutableVector<TNumeral> AOut)
        {
            Vector.Map(ALeft, AOut, AL => InnerGenericArithmetics.Divide(AL, ARight));
        }

        public static void Trunc<TNumeral>(
            IVector<TNumeral> AIn,
            IMutableVector<TNumeral> AOut)
        {
            Vector.Map(AIn, AOut, InnerGenericArithmetics.Trunc);
        }

        public static TNumeral StandardScalarProduct<TNumeral>(
            IVector<TNumeral> ALeft,
            IVector<TNumeral> ARight)
        {
            if (ALeft == null)
                throw new ArgumentNullException("ALeft");
            if (ARight == null)
                throw new ArgumentNullException("ARight");

            if (ALeft.Size != ARight.Size)
                throw new ArgumentException("Vector sizes do not match.");

            TNumeral tnResult = InnerGenericArithmetics.Zero<TNumeral>();
            for (int I = 0; I < ALeft.Size; I++)
                tnResult = InnerGenericArithmetics.Add(
                    tnResult,
                    InnerGenericArithmetics.Multiply(ALeft.Get(I), ARight.Get(I)));

            return tnResult;
        }
    }
}
