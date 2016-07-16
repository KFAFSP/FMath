using System;
using System.Reflection;

namespace FMath.Arithmetic
{
    public abstract class ArithmeticProvider<TNumeral>
    {
        public static readonly ArithmeticProvider<TNumeral> Instance;

        static ArithmeticProvider()
        {
            Type tNumeral = typeof(TNumeral);
            Assembly aAssembly = Assembly.GetCallingAssembly();

            foreach (Type tType in aAssembly.GetTypes())
            {
                if (!tType.IsClass || tType.IsAbstract)
                    continue;
                if (tType.BaseType == null)
                    continue;
                if (!tType.BaseType.IsConstructedGenericType || tType.BaseType.GenericTypeArguments.Length != 1)
                    continue;
                if (tType.BaseType.GenericTypeArguments[0] != tNumeral)
                    continue;
                if (!typeof(ArithmeticProvider<TNumeral>).IsAssignableFrom(tType))
                    continue;

                ConstructorInfo ciCreate = tType.GetConstructor(new Type[0]);

                if (ciCreate == null)
                    continue;

                ArithmeticProvider<TNumeral>.Instance = (ArithmeticProvider<TNumeral>)ciCreate.Invoke(new object[0]);
                return;
            }
        }

        public static bool Exists
        {
            get { return ArithmeticProvider<TNumeral>.Instance != null; }
        }

        public NaturalArithmeticProvider<TNumeral> AsNatural()
        {
            return this as NaturalArithmeticProvider<TNumeral>;
        }
        public IntegerArithmeticProvider<TNumeral> AsInteger()
        {
            return this as IntegerArithmeticProvider<TNumeral>;
        }
        public FractionalArithmeticProvider<TNumeral> AsFractional()
        {
            return this as FractionalArithmeticProvider<TNumeral>;
        }

        public abstract NumeralType Type { get; }
    }
}
