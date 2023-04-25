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

            Assert.AreEqual(_testCoordX, _testBall.CoordX);
            Assert.AreEqual(_testCoordY, _testBall.CoordY);
            Assert.AreEqual(_testRadius, _testBall.Radius);
        }

        [TestMethod]
        public void SetAttTest()
        {
            int newRadius = _testRadius + 1;
            int newCoordX = _testCoordX + 1;
            int newCoordY = _testCoordY + 1;

            _testBall.Radius = newRadius;
            _testBall.CoordX = newCoordX;
            _testBall.CoordY = newCoordY;

            
            Assert.AreEqual(newRadius, _testBall.Radius);
            Assert.AreEqual(newCoordY, _testBall.CoordY);   
            Assert.AreEqual(newCoordX, _testBall.CoordX);   
           
        }
    }
}