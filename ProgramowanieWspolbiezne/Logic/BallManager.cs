namespace Logic
{
    public class BallManager
    {
        private readonly int _boardHeight;
        private readonly int _boardWidth;
        private readonly int _ballRadius;
        private readonly Random _random;

        private Ball[] balls;

        public BallManager(int boardHeight, int boardWidth, int ballRadius)
        {
            _boardHeight = boardHeight;
            _boardWidth = boardWidth;
            _ballRadius = ballRadius;
            _random = new Random();
        }


        public Ball[] BallCreationRandomCoord(int howMany)
        {
            balls = new Ball[howMany];
            for (int i = 0; i < howMany; i++)
            {
                var (x, y) = GetRandPos();
                balls[i] = new(_ballRadius, x, y);

            }
            return balls;
        }

        private (int x, int y) GetRandPos()
        {
            int x = _random.Next(0 + _ballRadius, _boardWidth - _ballRadius);
            int y = _random.Next(0 + _ballRadius, _boardHeight - _ballRadius);
            return (x, y);
        }

    }
}