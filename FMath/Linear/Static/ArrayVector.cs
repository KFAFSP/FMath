using System;
using System.Linq;

using FMath.Linear.Generic.Immutable;

namespace FMath.Linear.Static
{
    public static class ArrayVector
    {
        public static ArrayVector<TData> Empty<TData>()
        {
            return new ArrayVector<TData>();
        }
        public static ArrayVector<TData> Zero<TData>(int ASize)
        {
            return ArrayVector.Filled<TData>(ASize);
        }
        public static ArrayVector<TData> Filled<TData>(int ASize, TData AFill = default(TData))
        {
            if (ASize < 0)
                throw new ArgumentOutOfRangeException("ASize");

            if (!AFill.Equals(default(TData)))
                return Enumerable.Repeat(AFill, ASize).ToArrayVector();

            return new ArrayVector<TData>(new TData[ASize], false);
        }
        public static ArrayVector<TData> Pack<TData>(params TData[] AData)
        {
            if (AData == null)
                throw new ArgumentNullException("AData");

            return new ArrayVector<TData>(AData);
        }
    }
}
