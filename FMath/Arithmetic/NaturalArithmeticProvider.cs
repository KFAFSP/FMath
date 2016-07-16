namespace FMath.Arithmetic
{
    public abstract class NaturalArithmeticProvider<TNumeral> :
        ArithmeticProvider<TNumeral>
    {
        public sealed class ULong : NaturalArithmeticProvider<ulong>
        {
            public override ulong Add(ulong ALeft, ulong ARight)
            {
                return ALeft + ARight;
            }
            public override ulong Multiply(ulong ALeft, ulong ARight)
            {
                return ALeft*ARight;
            }
            public override ulong Div(ulong ALeft, ulong ARight)
            {
                return ALeft/ARight;
            }
            public override ulong Mod(ulong ALeft, ulong ARight)
            {
                return ALeft%ARight;
            }

            public override bool IsModular { get { return true; } }
            public override ulong Zero { get { return 0UL; } }
            public override ulong One { get { return 1UL; } }

            public override NumeralType Type { get { return NumeralType.Natural; } }
        }

        public sealed class UInt : NaturalArithmeticProvider<uint>
        {
            public override uint Add(uint ALeft, uint ARight)
            {
                return unchecked (ALeft + ARight) % uint.MaxValue;
            }
            public override uint Multiply(uint ALeft, uint ARight)
            {
                return unchecked(ALeft * ARight) % uint.MaxValue;
            }
            public override uint Div(uint ALeft, uint ARight)
            {
                return ALeft / ARight;
            }
            public override uint Mod(uint ALeft, uint ARight)
            {
                return ALeft % ARight;
            }

            public override bool IsModular { get { return true; } }
            public override uint Zero { get { return 0U; } }
            public override uint One { get { return 1U; } }

            public override NumeralType Type { get { return NumeralType.Natural; } }
        }

        public sealed class UShort : NaturalArithmeticProvider<ushort>
        {
            public override ushort Add(ushort ALeft, ushort ARight)
            {
                return (ushort)(unchecked (ALeft + ARight) % ushort.MaxValue);
            }
            public override ushort Multiply(ushort ALeft, ushort ARight)
            {
                return (ushort)(unchecked(ALeft * ARight) % ushort.MaxValue);
            }
            public override ushort Div(ushort ALeft, ushort ARight)
            {
                return (ushort)(ALeft / ARight);
            }
            public override ushort Mod(ushort ALeft, ushort ARight)
            {
                return (ushort)(ALeft % ARight);
            }

            public override bool IsModular { get { return true; } }
            public override ushort Zero { get { return 0; } }
            public override ushort One { get { return 1; } }

            public override NumeralType Type { get { return NumeralType.Natural; } }
        }

        public sealed class Byte : NaturalArithmeticProvider<byte>
        {
            public override byte Add(byte ALeft, byte ARight)
            {
                return (byte)(unchecked(ALeft + ARight) % byte.MaxValue);
            }
            public override byte Multiply(byte ALeft, byte ARight)
            {
                return (byte)(unchecked(ALeft * ARight) % byte.MaxValue);
            }
            public override byte Div(byte ALeft, byte ARight)
            {
                return (byte)(ALeft / ARight);
            }
            public override byte Mod(byte ALeft, byte ARight)
            {
                return (byte)(ALeft % ARight);
            }

            public override bool IsModular { get { return true; } }
            public override byte Zero { get { return 0; } }
            public override byte One { get { return 1; } }

            public override NumeralType Type { get { return NumeralType.Natural; } }
        }

        public abstract TNumeral Add(TNumeral ALeft, TNumeral ARight);
        public abstract TNumeral Multiply(TNumeral ALeft, TNumeral ARight);
        public abstract TNumeral Div(TNumeral ALeft, TNumeral ARight);
        public abstract TNumeral Mod(TNumeral ALeft, TNumeral ARight);

        public abstract bool IsModular { get; }
        public abstract TNumeral Zero { get; }
        public abstract TNumeral One { get; }
    }
}