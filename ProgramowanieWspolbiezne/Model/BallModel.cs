using BallSimulator.Logic.API;
using BallSimulator.Model.API;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BallSimulator.Model
{
    public class BallModel : IBallModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;



        public float CoordinatX => _ball.Coordinates.X;
        public float CoordinatY => _ball.Coordinates.Y;
        public float TempoX => _ball.Tempo.X;
        public float TempoY => _ball.Tempo.Y;
        public int Diameter => _ball.Diameter;
        public int Radius => _ball.Radius;
        private IBallLogic _ball;
        private IDisposable? _unsubscriber;


        public BallModel(IBallLogic ball)
        {
            _ball = ball;
            Follow(_ball);
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
            RaisePropertyChanged(nameof(CoordinatX));
            RaisePropertyChanged(nameof(CoordinatY));
        }

        #endregion

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}