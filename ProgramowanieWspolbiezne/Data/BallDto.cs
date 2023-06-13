using Data.API;

namespace Data
{
    public class BallDto : IBall
    {
        public int Diameter { get; init; }
        public float TempoX { get; private set; }
        public float TempoY { get; private set; }
        public float CoordinatesX { get; private set; }
        public float CoordinatesY { get; private set; }

        private readonly ISet<IObserver<IBall>> _observers;

        public BallDto(int diameter, float coordinatesX, float coordinatesY, float tempoX, float tempoY)
        {
            Diameter = diameter;
            CoordinatesX = coordinatesX;
            CoordinatesY = coordinatesY;
            TempoX = tempoX;
            TempoY = tempoY;

            _observers = new HashSet<IObserver<IBall>>();
        }

        public async Task Move(float moveX, float moveY)
        {
            CoordinatesX += moveX;
            CoordinatesY += moveY;
            TrackBall(this);
            await Write();
        }

        public async Task SetTempo(float tempoX, float tempoY)
        {
            TempoX = tempoX;
            TempoY = tempoY;
            TrackBall(this);

            await Write();
        }

        private async Task Write()
        {
            await Task.Delay(1);
        }

        public IDisposable Subscribe(IObserver<IBall> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private void TrackBall(IBall ball)
        {
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
    }
}
