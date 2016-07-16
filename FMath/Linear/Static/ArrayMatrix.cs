using System;

using FMath.Linear.Generic.Immutable;

namespace FMath.Linear.Static
{
    public static class ArrayMatrix
    {
        public static ArrayMatrix<TData> Empty<TData>()
        {
            return new ArrayMatrix<TData>();
        }
        public static ArrayMatrix<TData> Zero<TData>(int ARowCount, int AColCount)
        {
            return ArrayMatrix.Filled<TData>(ARowCount, AColCount);
        }
        public static ArrayMatrix<TData> Filled<TData>(int ARowCount, int AColCount, TData AFill = default(TData))
        {
            if (ARowCount < 0)
                throw new ArgumentOutOfRangeException("ARowCount");
            if (AColCount < 0)
                throw new ArgumentOutOfRangeException("AColCount");
            if ((ARowCount == 0 && AColCount != 0) || (ARowCount != 0 && AColCount == 0))
                throw new ArgumentException("Matrix size may not be partially zero.");

            TData[,] aArray = new TData[ARowCount, AColCount];

            if (!AFill.Equals(default(TData)))
                aArray.Fill(AFill);

            return new ArrayMatrix<TData>(aArray, false);
        }
        public static ArrayMatrix<TData> Pack<TData>(int ARowCount, int AColCount, params TData[] ACells)
        {
            if (ARowCount < 0)
                throw new ArgumentOutOfRangeException("ARowCount");
            if (AColCount < 0)
                throw new ArgumentOutOfRangeException("AColCount");
            if (Math.Min(ARowCount, AColCount) == 0 && 0 != Math.Max(ARowCount, AColCount))
                throw new ArgumentException("Matrix size may not be partially zero.");

            if (ACells == null || ACells.Length != ARowCount * AColCount)
                throw new ArgumentException("Invalid cell data array.");

            TData[,] aArray = new TData[ARowCount, AColCount];

            int I = 0;
            for (int M = 0; M < ARowCount; M++)
                for (int N = 0; N < AColCount; N++)
                    aArray[M, N] = ACells[I++];

            return new ArrayMatrix<TData>(aArray, false);
        }
    }
}