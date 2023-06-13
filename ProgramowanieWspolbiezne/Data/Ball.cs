using Data.API;


namespace Data
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
        private readonly IDisposable? _disposer;

        private Vector2 _tempo;
        private Vector2 _coordinates;

        public Ball(int diameter, Vector2 coordinates, Vector2 tempo)
        {
            Diameter = diameter;
            Coordinates = coordinates;
            Tempo = tempo;
            Radius = diameter / 2;


            _observers = new HashSet<IObserver<IBall>>();
            _disposer = ThreadManager.Add<float>(Move);

        }

        public void Move(float delta)
        {

            if (Tempo.IsZero()) return;

            float strength = delta.Clamp(0f, 100f) * 0.01f;
            Coordinates += Tempo * strength;
        }

        public bool Touches(IBall ball)
        {
            int minDistance = this.Radius + ball.Radius;
            float minDistanceSquared = minDistance * minDistance;
            float actualDistanceSquared = Vector2.DistanceSquared(this.Coordinates, ball.Coordinates);

            return minDistanceSquared >= actualDistanceSquared;
        }

        #region Provider

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

        #endregion

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