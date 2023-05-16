using Presentation.ViewModel;
using System.ComponentModel;

namespace ViewModel
{
    public class StartSimCommand : CommandBase
    {
        private readonly SimViewModel _simulationViewModel;

        public StartSimCommand(SimViewModel simulationViewModel) : base()
        {
            _simulationViewModel = simulationViewModel;
            _simulationViewModel.PropertyChanged += OnSimViewModelPropertyChanged;

        }

        public override bool CanExecute(object? param)
        {
            return base.CanExecute(param) && !_simulationViewModel.IsSimRunning;
        }

        public override void Execute(object? param)
        {
            _simulationViewModel.StartSim();
        }

        public void OnSimViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SimViewModel.IsSimRunning))
            {
                OnCanExecuteChanged();
            }
        }
    }
}