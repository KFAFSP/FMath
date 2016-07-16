using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FMath.Arithmetics
{
    /// <summary>
    /// Abstract base class for arithmetic providers.
    /// </summary>
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

        /// <summary>
        /// Gets the provider for the specified type.
        /// </summary>
        /// <param name="AType">The type.</param>
        /// <returns>The provider instance, or null.</returns>
        public static ArithmeticProvider Get(Type AType)
        {
            ArithmeticProvider apProvider;
            if (!ArithmeticProvider._FProviders.TryGetValue(AType, out apProvider))
                return null;

            return apProvider;
        }
        /// <summary>
        /// Gets the strongly typed provider for the specified type.
        /// </summary>
        /// <typeparam name="TNumeral">The type.</typeparam>
        /// <returns>The provider instance, or null.</returns>
        public static ArithmeticProvider<TNumeral> Get<TNumeral>()
        {
            return ArithmeticProvider.Get(typeof(TNumeral)) as ArithmeticProvider<TNumeral>;
        }

        public abstract NumeralType NumberType { get; }
    }

    /// <summary>
    /// Abstract generic base class for arithmetic providers.
    /// </summary>
    /// <typeparam name="TNumeral">The type that is provided for.</typeparam>
    public abstract class ArithmeticProvider<TNumeral> :
        ArithmeticProvider
    {
        /// <summary>
        /// Gets the instance of this provider.
        /// </summary>
        /// <value>
        /// The instance of this provider.
        /// </value>
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