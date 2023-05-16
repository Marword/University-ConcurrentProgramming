using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests
{
    [TestClass]
    public class BallTest
    {
        private static readonly int _testCoordX = 2;
        private static readonly int _testCoordY = 2;
        private static readonly int _testDiameter = 1;
        private static readonly float _testTempoX = 0.3f;
        private static readonly float _testTempoY = -0.2f;


        private readonly Ball _testBall;
        private readonly Game _testGame;

        public BallTest()
        {
            Vector2 coordinates = new Vector2(_testCoordX, _testCoordY);
            Vector2 tempo = new Vector2(_testTempoX, _testTempoY);

            _testGame = new Game(100, 100);
            _testBall = new Ball(_testDiameter, coordinates, tempo, _testGame);
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

            Ball ball = new Ball(_testDiameter, new Vector2(_testCoordX, _testCoordY), Vector2.Zero, _testGame);

            ball.Tempo = new Vector2(0, -2.5f);
            Assert.AreEqual(ball.Tempo.X, 0f);

            ball.Move(delta);
            Assert.AreEqual(ball.Coordinates.X, _testCoordX);

            ball.Move(delta);
            ball.Move(delta);
            ball.Move(delta);

            ball.Tempo = new Vector2(3f, 5f);
            Assert.AreEqual(ball.Tempo, new Vector2(3, 5f));

            ball.Move(delta);
            Assert.AreEqual(ball.Coordinates.Y, 7f);

            ball.Move(delta);
            ball.Move(delta);
            ball.Move(delta);

            Assert.AreEqual(ball.Coordinates, new Vector2(14f, 22f));
        }

        [TestMethod]
        public void EqualTest()
        {
            Ball newBall = _testBall;
            Assert.AreEqual(_testBall, newBall);
        }
    }
}