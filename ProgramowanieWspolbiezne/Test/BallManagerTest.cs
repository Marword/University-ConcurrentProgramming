using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test
{
    [TestClass]
    public class BallManagerTest
    {
        private const int TestRadius = 2;
        private const int TestHeight = 100;
        private const int TestWidth = 100;
        private readonly BallManager _ballManager = new BallManager(new Game(TestHeight, TestWidth), TestRadius);


        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_ballManager);
        }

        [TestMethod]
        public void BallCreationRandomCoordTest()
        {
            Assert.IsNotNull(_ballManager);
            int ballNum = 10;
            Ball[] balls = _ballManager.BallCreationRandomCoord(ballNum);
            int count = 0;
            foreach (Ball b in balls)
            {
                Assert.IsNotNull(b);
                Assert.AreEqual(TestRadius, b.Radius);
                Assert.IsTrue(IsInside(b.Coordinates.X, 0, TestWidth));
                Assert.IsTrue(IsInside(b.Coordinates.Y, 0, TestHeight));
                count++;

            }
            Assert.AreEqual(ballNum, count);
        }

        private static bool IsInside(float value, float bottom, float top)
        {

            return value <= top - TestRadius && value >= bottom + TestRadius;
        }
    }
}