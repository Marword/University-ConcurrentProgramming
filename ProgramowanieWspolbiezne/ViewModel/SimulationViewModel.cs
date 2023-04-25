using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;


namespace ViewModel
{
    public class SimulationViewModel : ViewModelBase
    {
        public IEnumerable<BallModel> Balls => _balls;
        private readonly ObservableCollection<BallModel> _balls;
        private LogicModel _logic;
        private int _ballsCount;

        public SimulationViewModel(LogicModel logic)
        {
            _logic = logic;
        }

        public int BallsCount
        {
            get => _ballsCount;
            set
            {
                _ballsCount = value;
                RaisePropertyChanged(nameof(BallsCount));
            }
        }

        public CommandBase StartSimulation { get; }
    }
}