using Logic.API;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class BallModel : IBallModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;



        public Vector2 Coordinates => CalculateOffsetCoordinates(_ball.Coordinates);
        public Vector2 Tempo => _ball.Tempo;
        public int Diameter => _ball.Diameter;
        public int Radius => _ball.Radius;
        private IBallLogic _ball;
        private IDisposable? _unsubscriber;


        public BallModel(IBallLogic ball)
        {
            _ball = ball;
            Follow(_ball);
        }

        private Vector2 CalculateOffsetCoordinates(Vector2 coordinates)
        {
            return new Vector2(coordinates.X - Radius, coordinates.Y - Radius);
        }

        #region Observer


        public void Follow(IObservable<IBallLogic> privider)
        {
            _unsubscriber = privider.Subscribe(this);
        }

        public void OnCompleted()
        {
            _unsubscriber?.Dispose();
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(IBallLogic ball)
        {
            RaisePropertyChanged(nameof(Coordinates));
        }

        #endregion

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}