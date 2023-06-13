using BallSimulator.Logic.API;
using System.ComponentModel;

namespace BallSimulator.Model.API
{
    public interface IBallModel : IObserver<IBallLogic>, INotifyPropertyChanged
    {
        public int Diameter { get; }
        public int Radius { get; }
        public float TempoX { get; }
        public float TempoY { get; }
        public float CoordinatX { get; }
        public float CoordinatY { get; }
    }
}