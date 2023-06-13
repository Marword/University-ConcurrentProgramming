﻿using Logic.API;
using System.ComponentModel;

namespace Model
{
    public interface IBallModel : IObserver<IBallLogic>, INotifyPropertyChanged
    {
        public int Diameter { get; }
        public int Radius { get; }
        public Vector2 Tempo { get; }
        public Vector2 Coordinates { get; }
    }
}