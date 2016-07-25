using System;

namespace FMath
{
    public static class UtilityExtensions
    {
        public static bool Matches<TConstraint>(this object AValue)
        {
            return AValue is TConstraint || ((AValue == null) && typeof(TConstraint).IsByRef);
        }

        public static string GetNeatName(this Type AType)
        {
            string sName = AType.Name;
            int iGenArgIndex = sName.IndexOf('`');
            if (iGenArgIndex != -1)
                sName = sName.Substring(0, iGenArgIndex);

            return sName;
        }

        public static string SafeFormat(
            this object AObject,
            string AFormat,
            IFormatProvider AFormatProvider)
        {
            if (AObject == null)
                return "null";

            IFormattable ifObject = AObject as IFormattable;
            return ifObject != null
                ? ifObject.ToString(AFormat, AFormatProvider)
                : AObject.ToString();
        }
    }
}
