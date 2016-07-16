using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FMath.Arithmetics
{
    public abstract class ArithmeticProvider
    {
        private static Dictionary<Type, ArithmeticProvider> _FProviders;

        static ArithmeticProvider()
        {
            ArithmeticProvider._FProviders = new Dictionary<Type, ArithmeticProvider>();

            foreach (Type tProvider in Assembly.GetAssembly(typeof(ArithmeticProvider)).GetTypes())
            {
                if (!tProvider.IsClass || tProvider.IsAbstract)
                    continue;
                if (!typeof(ArithmeticProvider).IsAssignableFrom(tProvider))
                    continue;
                if (tProvider.BaseType == null)
                    continue;
                if (!tProvider.BaseType.IsConstructedGenericType || tProvider.BaseType.GenericTypeArguments.Length != 1)
                    continue;

                ArithmeticProvider._FProviders.Add(
                    tProvider.BaseType.GenericTypeArguments[0],
                    (ArithmeticProvider)tProvider.GetConstructor(new Type[]{}).Invoke(new object[]{}));
            }
        }
        public static ArithmeticProvider Get(Type AType)
        {
            ArithmeticProvider apProvider;
            if (!ArithmeticProvider._FProviders.TryGetValue(AType, out apProvider))
                return null;

            return apProvider;
        }
        public static ArithmeticProvider<TNumeral> Get<TNumeral>()
        {
            return ArithmeticProvider.Get(typeof(TNumeral)) as ArithmeticProvider<TNumeral>;
        }

        public abstract NumeralType NumberType { get; }
    }
    public abstract class ArithmeticProvider<TNumeral> :
        ArithmeticProvider
    {
        public static ArithmeticProvider<TNumeral> Instance { get { return ArithmeticProvider.Get<TNumeral>(); } }

        public static NaturalArithmeticProvider<TNumeral> AsNatural
        {
            get { return ArithmeticProvider<TNumeral>.Instance as NaturalArithmeticProvider<TNumeral>; }
        }
        public static IntegerArithmeticProvider<TNumeral> AsInteger
        {
            get { return ArithmeticProvider<TNumeral>.Instance as IntegerArithmeticProvider<TNumeral>; }
        }
        public static RealArithmeticProvider<TNumeral> AsReal
        {
            get { return ArithmeticProvider<TNumeral>.Instance as RealArithmeticProvider<TNumeral>; }
        }
    }
}