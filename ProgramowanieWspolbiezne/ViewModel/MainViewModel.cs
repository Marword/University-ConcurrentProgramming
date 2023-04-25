﻿using Presentation.Model;

namespace Presentation.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel() : base()
        {
            CurrentViewModel = new SimViewModel(
                ballsCountChecker: new BallsCountChecker(1, 20)
                );
        }
    }
}