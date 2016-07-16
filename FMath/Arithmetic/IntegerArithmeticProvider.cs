using System;

namespace FMath.Arithmetic
{
    public abstract class IntegerArithmeticProvider<TNumeral> :
        NaturalArithmeticProvider<TNumeral>
    {
        public sealed class Long : IntegerArithmeticProvider<long>
        {
            public override long Add(long ALeft, long ARight)
            {
                return ALeft + ARight;
            }
            public override long Multiply(long ALeft, long ARight)
            {
                return ALeft*ARight;
            }
            public override long Div(long ALeft, long ARight)
            {
                return ALeft/ARight;
            }
            public override long Mod(long ALeft, long ARight)
            {
                return ALeft%ARight;
            }
            
            public override bool IsModular { get { return true; } }
            public override long Zero { get { return 0L; } }
            public override long One { get { return 1L; } }

            public override long Sign(long ALeft)
            {
                return Math.Sign(ALeft);
            }
            public override long Absolute(long ALeft)
            {
                return Math.Abs(ALeft);
            }
            public override long Negate(long ALeft)
            {
                return -ALeft;
            }

            public override NumeralType Type { get { return NumeralType.Integer; } }
        }

        public sealed class Int : IntegerArithmeticProvider<int>
        {
            public override int Add(int ALeft, int ARight)
            {
                return unchecked (ALeft + ARight) % int.MaxValue;
            }
            public override int Multiply(int ALeft, int ARight)
            {
                return unchecked(ALeft * ARight) % int.MaxValue;
            }
            public override int Div(int ALeft, int ARight)
            {
                return ALeft / ARight;
            }
            public override int Mod(int ALeft, int ARight)
            {
                return ALeft % ARight;
            }

            public override bool IsModular { get { return true; } }
            public override int Zero { get { return 0; } }
            public override int One { get { return 1; } }

            public override int Sign(int ALeft)
            {
                return Math.Sign(ALeft);
            }
            public override int Absolute(int ALeft)
            {
                return Math.Abs(ALeft);
            }
            public override int Negate(int ALeft)
            {
                return -ALeft;
            }

            public override NumeralType Type { get { return NumeralType.Integer; } }
        }

        public sealed class Short : IntegerArithmeticProvider<short>
        {
            public override short Add(short ALeft, short ARight)
            {
                return (short)(unchecked(ALeft + ARight) % short.MaxValue);
            }
            public override short Multiply(short ALeft, short ARight)
            {
                return (short)(unchecked(ALeft * ARight) % short.MaxValue);
            }
            public override short Div(short ALeft, short ARight)
            {
                return (short)(ALeft / ARight);
            }
            public override short Mod(short ALeft, short ARight)
            {
                return (short)(ALeft % ARight);
            }

            public override bool IsModular { get { return true; } }
            public override short Zero { get { return 0; } }
            public override short One { get { return 1; } }

            public override short Sign(short ALeft)
            {
                return (short)Math.Sign(ALeft);
            }
            public override short Absolute(short ALeft)
            {
                return Math.Abs(ALeft);
            }
            public override short Negate(short ALeft)
            {
                return (short)-ALeft;
            }

            public override NumeralType Type { get { return NumeralType.Integer; } }
        }

        public sealed class SByte : IntegerArithmeticProvider<sbyte>
        {
            public override sbyte Add(sbyte ALeft, sbyte ARight)
            {
                return (sbyte)(unchecked(ALeft + ARight) % sbyte.MaxValue);
            }
            public override sbyte Multiply(sbyte ALeft, sbyte ARight)
            {
                return (sbyte)(unchecked(ALeft * ARight) % sbyte.MaxValue);
            }
            public override sbyte Div(sbyte ALeft, sbyte ARight)
            {
                return (sbyte)(ALeft / ARight);
            }
            public override sbyte Mod(sbyte ALeft, sbyte ARight)
            {
                return (sbyte)(ALeft % ARight);
            }

            public override bool IsModular { get { return true; } }
            public override sbyte Zero { get { return 0; } }
            public override sbyte One { get { return 1; } }

            public override sbyte Sign(sbyte ALeft)
            {
                return (sbyte)Math.Sign(ALeft);
            }
            public override sbyte Absolute(sbyte ALeft)
            {
                return Math.Abs(ALeft);
            }
            public override sbyte Negate(sbyte ALeft)
            {
                return (sbyte)-ALeft;
            }

            public override NumeralType Type { get { return NumeralType.Integer; } }
        }

        public abstract TNumeral Sign(TNumeral ALeft);
        public abstract TNumeral Absolute(TNumeral ALeft);
        public abstract TNumeral Negate(TNumeral ALeft);

        public virtual TNumeral Subtract(TNumeral ALeft, TNumeral ARight)
        {
            return this.Add(ALeft, this.Negate(ARight));
        }

        public virtual TNumeral NegativeOne { get { return this.Negate(this.One); } }
    }
}