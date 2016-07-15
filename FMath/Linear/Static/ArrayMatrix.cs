using System;

using FMath.Linear.Generic.Immutable;

namespace FMath.Linear.Static
{
    /// <summary>
    /// Static factory class for <see cref="ArrayMatrix{TData}"/> structs.
    /// </summary>
    public static class ArrayMatrix
    {
        /// <summary>
        /// Gets an empty <see cref="ArrayMatrix{TData}"/> struct.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <returns>The empty matrix.</returns>
        public static ArrayMatrix<TData> Empty<TData>()
        {
            return new ArrayMatrix<TData>();
        }

        /// <summary>
        /// Gets a <see cref="ArrayMatrix{TData}" /> that is filled with the default (zero) element of the stored type.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="ARowCount">The number of rows.</param>
        /// <param name="AColCount">The number of columns.</param>
        /// <returns>
        /// The zero matrix.
        /// </returns>
        public static ArrayMatrix<TData> Zero<TData>(int ARowCount, int AColCount)
        {
            return ArrayMatrix.Filled<TData>(ARowCount, AColCount);
        }

        /// <summary>
        /// Gets a <see cref="ArrayMatrix{TData}"/> that is filled with the specified default element.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="ARowCount">The number of rows.</param>
        /// <param name="AColCount">The number of columns.</param>
        /// <param name="AFill">The fill element.</param>
        /// <returns>The filled matrix.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the size is out of range.</exception>
        /// <exception cref="System.ArgumentException">Matrix size may not be partially zero.</exception>
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

        /// <summary>
        /// Packs the specified data into an <see cref="ArrayMatrix{TData}" />.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="ARowCount">The number of rows.</param>
        /// <param name="AColCount">The number of columns.</param>
        /// <param name="ACells">The cell data.</param>
        /// <returns>
        /// The packed matrix.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the size is out of range.</exception>
        /// <exception cref="System.ArgumentException">Matrix size may not be partially zero and the cell data array must have exactly ARowCount*ACellCount entries.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when AData is null.</exception>
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