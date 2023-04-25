using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class BallTest
    {
        readonly int _testCoordX;
        readonly int _testCoordY;
        readonly int _testRadius;
        readonly float _testTempoX;
        readonly float _testTempoY;


        readonly Ball _testBall;

        public BallTest()
        {
            _testCoordX = 2;
            _testCoordY = 2;
            _testRadius = 1;
            _testTempoX = 0.3f;
            _testTempoY = -0.2f;

            Vector2 coordinates = new Vector2(_testCoordX, _testCoordY);
            Vector2 tempo = new Vector2(_testTempoX, _testTempoY);

            _testBall = new Ball(_testRadius, coordinates, tempo);
        }


        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_testBall);

            Assert.AreEqual(_testCoordX, _testBall.Coordinates.X);
            Assert.AreEqual(_testCoordY, _testBall.Coordinates.Y);
            Assert.AreEqual(_testRadius, _testBall.Radius);
        }

        [TestMethod]
        public void SetAttributesTest()
        {
            int diff = 1;
            int newXPos = _testCoordX + diff;
            int newYPos = _testCoordY + diff;
            Vector2 newPos = new Vector2(newXPos, newYPos);

            _testBall.Coordinates = newPos;
            Assert.AreEqual(newPos, _testBall.Coordinates);
        }

        [TestMethod]
        public void MoveTest()
        {
            Vector2 boundX = new Vector2(0, 100);
            Vector2 boundY = new Vector2(0, 100);
            Vector2 tempo = new Vector2(-2.5f, 0);

            _testBall.Tempo = tempo;

            _testBall.Move(boundX, boundY, 1);
            Assert.AreEqual(_testBall.Coordinates.X, _testCoordX - 2.5);

            _testBall.Move(boundX, boundY, 1);
            _testBall.Move(boundX, boundY, 1);
            _testBall.Move(boundX, boundY, 1);

            Assert.AreEqual(_testBall.Tempo.X, -tempo.X);

            tempo = new Vector2(0, -2.5f);
            _testBall.Tempo = tempo;

            _testBall.Move(boundX, boundY, 1);
            Assert.AreEqual(_testBall.Coordinates.Y, _testCoordY - 2.5);

            _testBall.Move(boundX, boundY, 1);
            _testBall.Move(boundX, boundY, 1);
            _testBall.Move(boundX, boundY, 1);

            Assert.AreEqual(_testBall.Tempo, -tempo);

            Assert.ThrowsException<ArgumentException>(() => _testBall.Move(boundX, boundY, 2));
        }

        [TestMethod]
        public void EqualTest()
        {
            Ball newBall = _testBall;
            Assert.AreEqual(_testBall, newBall);
        }
    }
}
