namespace Logic
{
    public class BallManager
    {
        private readonly Game _game;
        private readonly int _ballRadius;
        private readonly Random _random;

        public Ball[] Balls { get; set; }

        public BallManager(Game game, int ballRadius)
        {
            _game = game;
            _ballRadius = ballRadius;
            _random = new Random();
        }


        public Ball[] BallCreationRandomCoord(int howMany)
        {
            Balls = new Ball[howMany];
            for (int i = 0; i < howMany; i++)
            {
                var (coordX, coordY) = GetRandPos();
                var (tempoX, tempoY) = GetRandTempo();
                Balls[i] = new(_ballRadius, coordX, coordY, tempoX, tempoY);

            }
            return Balls;
        }

        private (int x, int y) GetRandPos()
        {
            int x = _random.Next(0 + _ballRadius, _game.Width - _ballRadius);
            int y = _random.Next(0 + _ballRadius, _game.Height - _ballRadius);
            return (x, y);
        }

        private (float x, float y) GetRandTempo()
        {
            double x = _random.NextDouble() * 20 - 10;
            double y = _random.NextDouble() * 20 - 10;

            return ((float)x, (float)y);
        }

        public void PushAllBalls(float power = 0.2f)
        {
            foreach (var ball in Balls)
            {
                ball.Move(_game.boundX, _game.boundY, power);
            }
        }

    }
}