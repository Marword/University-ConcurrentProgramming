using System.ComponentModel;

namespace Presentation.ViewModel
{
    public class StopSimCommand : CommandBase
    {
        private readonly SimViewModel _simulationViewModel;

        public StopSimCommand(SimViewModel simulationViewModel) : base()
        {
            _simulationViewModel = simulationViewModel;

            _simulationViewModel.PropertyChanged += OnSimViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter)
                && _simulationViewModel.IsSimulationOn;
        }

        public override void Execute(object parameter)
        {
            _simulationViewModel.StopSim();
        }

        private void OnSimViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_simulationViewModel.IsSimulationOn))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
