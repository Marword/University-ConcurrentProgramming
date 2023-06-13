using System.ComponentModel;

namespace BallSimulator.ViewModel
{
    public class StopSimCommand : CommandBase
    {
        private readonly SimViewModel _simulationViewModel;

        public StopSimCommand(SimViewModel simulationViewModel) : base()
        {
            _simulationViewModel = simulationViewModel;

            _simulationViewModel.PropertyChanged += OnSimViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && _simulationViewModel.IsSimRunning;
        }

        public override void Execute(object? parameter)
        {
            _simulationViewModel.StopSim();
        }

        private void OnSimViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_simulationViewModel.IsSimRunning))
            {
                OnCanExecuteChanged();
            }
        }
    }
}