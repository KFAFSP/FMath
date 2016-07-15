using System;

namespace FMath.Linear.Static
{
    public static class Matrix
    {
        public const int C_HashSaltPrime = 499;

        public static bool AreDefined(
            this IMatrix AMatrix,
            MatrixIndices AIndices)
        {
            if (AMatrix == null)
                throw new ArgumentNullException("AMatrix");

            return AIndices.M >= 0 && AIndices.N >= 0
                   && AIndices.M < AMatrix.Size.M && AIndices.N < AMatrix.Size.N;
        }

        #region Basic operations
        public static bool AreEqual(IMatrix ALeft, IMatrix ARight)
        {
            // TODO : Implement
            return false;
        }
        public static int Hash(IMatrix AVector)
        {
            // TODO : Implement
            return 0;
        }
        #endregion
    }
}
