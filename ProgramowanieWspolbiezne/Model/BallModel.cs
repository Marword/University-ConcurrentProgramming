using Logic;

namespace Model
{
    public class BallModel
    {
        private readonly Ball _ball;

        public int Radius => _ball.Radius;
        public Vector2 Position => _ball.Position;


        public BallModel(Ball ball) { _ball = ball; }

    }
}