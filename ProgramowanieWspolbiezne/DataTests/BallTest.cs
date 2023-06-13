using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests
{
    [TestClass]
    public class BallTest
    {
        private static readonly int _testCoordX = 5;
        private static readonly int _testCoordY = 5;
        private static readonly int _testDiameter = 1;
        private static readonly Vector2 Coordinates = new(_testCoordX, _testCoordY);
        private static readonly Vector2 Tempo = new(_testTempoX, _testTempoY);
        private static readonly float _testTempoX = 0.3f;
        private static readonly float _testTempoY = -0.2f;


        private readonly Ball _testBall;

        public BallTest()
        {
            _testBall = new Ball(_testDiameter, Coordinates, Tempo);
        }


        [TestMethod]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_testBall);

            Assert.AreEqual(_testCoordX, _testBall.Coordinates.X);
            Assert.AreEqual(_testCoordY, _testBall.Coordinates.Y);
            Assert.AreEqual(_testDiameter, _testBall.Diameter);
        }


        [TestMethod]
        public void MoveTest()
        {
            float delta = 100f;

            Ball ball = new Ball(_testDiameter, Coordinates, Vector2.Zero)
            {
                Tempo = new Vector2(0, -2.5f)
            };


            Assert.AreEqual(ball.Tempo.X, 0f);

            ball.Move(delta);
            Assert.AreEqual(ball.Coordinates.X, _testCoordX);

            ball.Move(delta);
            ball.Move(delta);
            ball.Move(delta);

            ball.Tempo = new Vector2(3f, 5f);
            Assert.AreEqual(ball.Tempo, new Vector2(3, 5f));

            ball.Move(delta);
            Assert.AreEqual(ball.Coordinates.Y, 0f);

            ball.Move(delta);
            ball.Move(delta);
            ball.Move(delta);

            Assert.AreEqual(ball.Coordinates, new Vector2(17f, 15f));
        }

        [TestMethod]
        public void EqualTest()
        {
            Ball newBall = _testBall;
            Assert.AreEqual(_testBall, newBall);
        }
    }
}