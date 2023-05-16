using Data;


namespace Logic
{
    public class Ball : IBall, IEquatable<Ball>
    {
        private readonly object coordinatesLock = new();
        private readonly object tempoLock = new();

        public int Diameter { get; init; }
        public int Radius { get; init; }
        public Vector2 Tempo
        {
            get
            {
                lock (tempoLock)
                {
                    return _tempo;
                }
            }
            set
            {
                lock (tempoLock)
                {
                    _tempo = value;
                }
            }
        }
        public Vector2 Coordinates
        {
            get
            {
                lock (coordinatesLock)
                {
                    return _coordinates;
                }
            }
            private set
            {
                lock (coordinatesLock)
                {
                    if (_coordinates == value) return;
                    _coordinates = value;
                    TrackBall(this);
                }
            }
        }

        private readonly ISet<IObserver<IBall>> _observers;
        private Game _game;
        private readonly IBallDto _ballDto;

        private IDisposable? _disposer;
        private IDisposable? _unsubscriber;
        private Vector2 _tempo;
        private Vector2 _coordinates;


        public Ball(int diameter, int coordX, int coordY, float tempoX, float tempoY, Game game, IBallDto? ballDto = default)
            : this(diameter, new Vector2(coordX, coordY), new Vector2(tempoX, tempoY), game, ballDto) { }
        public Ball(int diameter, Vector2 coordinates, Vector2 tempo, Game game, IBallDto? ballDto = default)
        {
            Diameter = diameter;
            Coordinates = coordinates;
            Tempo = tempo;
            Radius = diameter / 2;
            _game = game;
            _ballDto = ballDto ?? new BallDto(Diameter, Coordinates.X, Coordinates.Y, Tempo.X, Tempo.Y);

            _observers = new HashSet<IObserver<IBall>>();
            _disposer = ThreadManager.Add<float>(Move);
            Follow(_ballDto);
        }

        public void Move(float delta)
        {

            if (Tempo.IsZero()) return;

            float strength = (delta * 0.01f).Clamp(0f, 1f);

            var move = Tempo * strength;
            var (coordX, coordY) = Coordinates;
            var (newTempoX, newTempoY) = Tempo;
            var (boundXx, boundXy) = _game.boundX;

            if (!coordX.Inside(boundXx, boundXy, Radius))
            {
                if (coordX <= boundXx + Radius)
                {
                    newTempoX = MathF.Abs(newTempoX);
                }
                else
                {
                    newTempoX = -MathF.Abs(newTempoX);
                }

            }
            var (boundYx, boundYy) = _game.boundY;

            if (!coordY.Inside(boundYx, boundYy, Radius))
            {
                if (coordY <= boundYx + Radius)
                {
                    newTempoY = MathF.Abs(newTempoY);
                }
                else
                {
                    newTempoY = -MathF.Abs(newTempoY);
                }

            }

            _ballDto?.SetTempo(newTempoX, newTempoY);
            _ballDto?.Move(move.X, move.Y);
        }

        public bool Touches(IBall ball)
        {
            int minDistance = this.Radius + ball.Radius;
            float minDistanceSquared = minDistance * minDistance;
            float actualDistanceSquared = Vector2.DistanceSquared(this.Coordinates, ball.Coordinates);

            return minDistanceSquared >= actualDistanceSquared;
        }

        public void Follow(IObservable<IBallDto> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void OnCompleted()
        {
            _unsubscriber?.Dispose();
        }

        public void OnError(Exception error) => throw error;

        public void OnNext(IBallDto ballDto)
        {
            Coordinates = new Vector2(ballDto.CoordinatesX, ballDto.CoordinatesY);
            Tempo = new Vector2(ballDto.TempoX, ballDto.TempoY);
        }

        public IDisposable Subscribe(IObserver<IBall> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public void TrackBall(IBall ball)
        {
            if (_observers == null) return;
            foreach (var observer in _observers)
            {
                observer.OnNext(ball);
            }
        }



        private class Unsubscriber : IDisposable
        {
            private readonly ISet<IObserver<IBall>> _observers;
            private readonly IObserver<IBall> _observer;

            public Unsubscriber(ISet<IObserver<IBall>> observers, IObserver<IBall> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                _observers.Remove(_observer);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _disposer?.Dispose();
        }

        public override bool Equals(object? obj)
        {
            return obj is Ball ball
                && Equals(ball);
        }

        public bool Equals(Ball? other)
        {
            return other is not null
                && Diameter == other.Diameter
                && Coordinates == other.Coordinates
                && Tempo == other.Tempo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Diameter, Coordinates, Tempo);
        }

        public override string? ToString()
        {
            return $"Ball d={Diameter}, P=[{Coordinates.X:n1}, {Coordinates.Y:n1}], S=[{Tempo.X:n1}, {Tempo.Y:n1}]";
        }
    }
}