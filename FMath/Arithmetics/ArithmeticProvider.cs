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

            foreach (Type tProvider in Assembly.GetAssembly(typeof(ArithmeticProvider)).GetTypes()
                .Where(AType => AType != typeof(ArithmeticProvider)
                    && typeof(ArithmeticProvider).IsAssignableFrom(AType)
                    && AType.IsClass
                    && !AType.IsAbstract
                    && AType.BaseType != null
                    && AType.BaseType.IsConstructedGenericType
                    && AType.BaseType.GenericTypeArguments.Length == 1))
            {
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
        /// <typeparam name="TType">The type.</typeparam>
        /// <returns>The provider instance, or null.</returns>
        public static ArithmeticProvider<TType> Get<TType>()
        {
            return ArithmeticProvider.Get(typeof(TType)) as ArithmeticProvider<TType>;
        }
    }

    /// <summary>
    /// Abstract generic base class for arithmetic providers.
    /// </summary>
    /// <typeparam name="TType">The type that is provided for.</typeparam>
    public abstract class ArithmeticProvider<TType> :
        ArithmeticProvider
    {
        /// <summary>
        /// Gets the instance of this provider.
        /// </summary>
        /// <value>
        /// The instance of this provider.
        /// </value>
        public static ArithmeticProvider<TType> Instance { get { return ArithmeticProvider.Get<TType>(); } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ArithmeticProvider{TType}"/> is exists.
        /// </summary>
        /// <value>
        ///   <c>true</c> if exists, <c>false</c> otherwise.
        /// </value>
        public static bool Exists { get { return ArithmeticProvider<TType>.Instance != null; } }
        /// <summary>
        /// Gets a value indicating whether this provider supports natural operations.
        /// </summary>
        /// <value>
        /// <c>true</c> if this provider supports natural operations, <c>false</c> otherwise.
        /// </value>
        public static bool IsNatural { get { return ArithmeticProvider<TType>.Instance is NaturalArithmeticProvider<TType>; } }
        /// <summary>
        /// Gets a value indicating whether this provider supports integer operations.
        /// </summary>
        /// <value>
        /// <c>true</c> if this provider supports integer operations, <c>false</c> otherwise.
        /// </value>
        public static bool IsInteger { get { return ArithmeticProvider<TType>.Instance is IntegerArithmeticProvider<TType>; } }
        /// <summary>
        /// Gets a value indicating whether this provider supports real operations.
        /// </summary>
        /// <value>
        /// <c>true</c> if this provider supports real operations, <c>false</c> otherwise.
        /// </value>
        public static bool IsReal { get { return ArithmeticProvider<TType>.Instance is RealArithmeticProvider<TType>; } }
    }
}