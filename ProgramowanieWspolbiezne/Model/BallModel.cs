using Logic;

namespace Presentation.Model
{
    public class BallModel
    {
        public readonly Ball _ball;

        public Vector2 Coordinates => _ball.Coordinates;
        public Vector2 Tempo => _ball.Tempo;
        public int Radius => _ball.Radius;


        public BallModel(Ball ball) { _ball = ball; }

    }
}