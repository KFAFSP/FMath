using FMath.Linear.Generic.Immutable;
using FMath.Linear.Static;

using NUnit.Framework;

namespace TestApp.Tests.Linear
{
    [TestFixture]
    public class ArrayVectorTests
    {
        [Test]
        public void Immutability()
        {
            int[] aTest = new[] { 1, 1, 1 };
            ArrayVector<int> avTest = new ArrayVector<int>(aTest);

            Assert.AreEqual(1, avTest[1]);

            aTest[1] = 15;

            Assert.AreEqual(1, avTest[1]);
        }

        [Test]
        public void ReferenceBasedMutability()
        {
            int[] aTest = new[] {1, 1, 1};
            ArrayVector<int> avTest = new ArrayVector<int>(aTest, false);

            Assert.AreEqual(1, avTest[1]);

            aTest[1] = 15;

            Assert.AreEqual(15, avTest[1]);
        }

        [Test]
        public void Equatability()
        {
            ArrayVector<int> av1 = ArrayVector.Pack(1, 2, 3);
            ArrayVector<int> av2 = ArrayVector.Pack(1, 2, 3);
            ArrayVector<int> av3 = ArrayVector.Pack(1, 2, 3, 4);
            ArrayVector<int> av4 = ArrayVector.Pack(1, 2, 4);

            Assert.IsTrue(av1.Equals(av2));
            Assert.IsFalse(av1.Equals(av3));
            Assert.IsFalse(av1.Equals(av4));
        }
    }
}
