using System;

namespace FMath.Linear.Static
{
    public static class Vector
    {
        public const int C_HashSaltPrime = 409;

        public static bool IsDefined(
            this IVector AVector,
            int AIndex)
        {
            if (AVector == null)
                throw new ArgumentNullException("AVector");

            return AIndex >= 0 && AIndex < AVector.Size;
        }

        #region Basic operations
        public static bool AreEqual(IVector ALeft, IVector ARight)
        {
            // TODO : Implement
            return false;
        }
        public static int Hash(IVector AVector)
        {
            // TODO : Implement
            return 0;
        }
        #endregion
    }
}
