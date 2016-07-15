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

            foreach (Type tProvider in Assembly.GetAssembly(typeof(ArithmeticProvider)).GetTypes()
                .Where(AType => AType != typeof(ArithmeticProvider)
                    && typeof(ArithmeticProvider).IsAssignableFrom(AType)
                    && AType.IsClass
                    && !AType.IsAbstract))
            {
                ArithmeticProvider._FProviders.Add(
                    tProvider.GenericTypeArguments[0],
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
        public static ArithmeticProvider<TType> Get<TType>()
        {
            return ArithmeticProvider.Get(typeof(TType)) as ArithmeticProvider<TType>;
        }
    }

    public abstract class ArithmeticProvider<TType> :
        ArithmeticProvider
    {
        public static readonly ArithmeticProvider<TType> Instance = ArithmeticProvider.Get<TType>();

        public static bool Exists { get { return ArithmeticProvider<TType>.Instance != null; } }
        public static bool IsNatural { get { return ArithmeticProvider<TType>.Instance is NaturalArithmeticProvider<TType>; } }
        public static bool IsInteger { get { return ArithmeticProvider<TType>.Instance is IntegerArithmeticProvider<TType>; } }
        public static bool IsReal { get { return ArithmeticProvider<TType>.Instance is RealArithmeticProvider<TType>; } }
    }
}