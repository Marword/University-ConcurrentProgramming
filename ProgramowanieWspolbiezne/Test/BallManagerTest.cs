using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
namespace Test
{
    [TestClass]
    public class BallManagerTest
    {
        readonly int _testRadius;
        readonly int _testHeight;
        readonly int _testWidth;
        readonly BallManager _ballManager;

        public BallManagerTest()
        {
            _testHeight = 100;
            _testWidth = 100;
            _testRadius = 2;
            _ballManager = new(_testHeight, _testWidth, _testRadius);
        }

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
                Assert.AreEqual(_testRadius, b.Radius);
                Assert.IsTrue(IsBetween(b.CoordX, 0, _testWidth));
                Assert.IsTrue(IsBetween(b.CoordY, 0, _testHeight));
                count++;

            }
            Assert.AreEqual(ballNum, count);
        }

        private bool IsBetween(int value, int bottom, int top)
        {
            if (value <= top - _testRadius && value >= bottom + _testRadius)
            {
                return true;
            }
            return false;
        }
    }
}