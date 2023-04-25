using Microsoft.VisualStudio.TestTools.UnitTesting;

using Logic;

namespace Test
{
    [TestClass]
    public class BallTest
    {
        readonly int _testCoordX;
        readonly int _testCoordY;
        readonly int _testRadius;


        readonly Ball _testBall;

        public BallTest()
        {
            _testCoordX = 2;
            _testCoordY = 2;
            _testRadius = 1;

            _testBall = new Ball(_testRadius, _testCoordX, _testCoordY);
        }


        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_testBall);

            Assert.AreEqual(_testCoordX, _testBall.Position.X);
            Assert.AreEqual(_testCoordY, _testBall.Position.Y);
            Assert.AreEqual(_testRadius, _testBall.Radius);
        }

        [TestMethod]
        public void SetAttTest()
        {
            int newCoordX = _testCoordX + 1;
            int newCoordY = _testCoordY + 1;

            Vector2 newPos = new Vector2(newCoordY, newCoordX);

            Assert.AreEqual(newPos, _testBall.Position);

        }
    }
}