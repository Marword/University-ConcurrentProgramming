using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class V2Test
    {
        private const float A = 2f;
        private const float B = 0.4f;
        private Vector2 Vector1 = new Vector2(A, B);
        private Vector2 Vector2 = new Vector2(B, A);

        [TestMethod]
        public void ConstructorTest()
        {
            Assert.AreEqual(Vector1.X, A);
            Assert.AreEqual(Vector1.Y, B);

            Assert.AreEqual(Vector2.X, B);
            Assert.AreEqual(Vector2.Y, A);
        }


    }
}