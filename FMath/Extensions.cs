using System;
using System.Collections.Generic;
using System.Linq;

using FMath.Linear.Generic.Immutable;

namespace FMath
{
    public static class Extensions
    {
        #region IEnumerable
        public static ArrayVector<TData> ToArrayVector<TData>(this IEnumerable<TData> AEnumerable)
        {
            if (AEnumerable == null)
                throw new ArgumentNullException("AEnumerable");

            return AEnumerable.ToArray().AsArrayVector();
        }
        #endregion

        #region Array
        public static ArrayVector<TData> ToArrayVector<TData>(this TData[] AArray)
        {
            if (AArray == null)
                throw new ArgumentNullException("AArray");

            return new ArrayVector<TData>(AArray, true);
        }
        public static ArrayVector<TData> AsArrayVector<TData>(this TData[] AArray)
        {
            if (AArray == null)
                throw new ArgumentNullException("AArray");

            return new ArrayVector<TData>(AArray, false);
        }

        public static void Fill(this Array AArray, object AValue)
        {
            if (AArray == null)
                throw new ArgumentNullException("AArray");

            int[] aStarts = Enumerable.Range(0, AArray.Rank).Select(AArray.GetLowerBound).ToArray();
            int[] aDims = Enumerable.Range(0, AArray.Rank).Select(AArray.GetLength).ToArray();
            int[] aEndPlusOne = aStarts.Zip(aDims, (AL, AR) => AL + AR).ToArray();

            int[] aCurrent = (int[])aStarts.Clone();

            while (aCurrent.Zip(aEndPlusOne, (AL, AR) => AL - AR).All(ADiff => ADiff < 0))
            {
                AArray.SetValue(AValue, aCurrent);

                for (int I = 0; I <= aDims.Length; I++)
                {
                    if (I == aDims.Length)
                        return;

                    aCurrent[I]++;
                    if (aCurrent[I] == aEndPlusOne[I])
                        aCurrent[I] = aStarts[I];
                    else
                        break;
                }
            }
        }
        #endregion
    }
}
