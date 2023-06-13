﻿using BallSimulator.Data;
using BallSimulator.Data.API;
using System.ComponentModel;

namespace BallSimulator.Logic.API
{
    public interface IBallLogic : IObservable<IBallLogic>, IObserver<IBall>, INotifyPropertyChanged
    {
        public int Diameter { get; }
        public int Radius { get; }
        public Vector2 Tempo { get; }
        public Vector2 Coordinates { get; }
    }
}