using Presentation.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;


namespace Presentation.ViewModel
{
    public class SimViewModel : ViewModelBase
    {

        private ObservableCollection<BallModel> _balls;
        private readonly ModelApi _logic;
        private readonly IChecker<int> _ballsCountChecker;
        private int _ballsCount = 10;
        private bool _isSimulationOn = false;
        public IEnumerable<BallModel> Balls => _balls;
        public ICommand StartSimCommand { get; }
        public ICommand StopSimCommand { get; }

        public int BallsCount
        {
            get => _ballsCount;
            set
            {
                if (_ballsCountChecker.Check(value))
                {
                    _ballsCount = value;
                    RaisePropertyChanged(nameof(BallsCount));
                }
                else _ballsCount = 1;

            }
        }

        public bool IsSimulationOn
        {
            get => _isSimulationOn;
            private set
            {
                _isSimulationOn = value;
                RaisePropertyChanged(nameof(IsSimulationOn));
            }
        }

        public SimViewModel(ModelApi model = default, IChecker<int> ballsCountChecker = default) : base()
        {
            _logic = model ?? ModelApi.CreateModelApi();
            _ballsCountChecker = ballsCountChecker ?? new BallsCountChecker();
            StartSimCommand = new StartSimCommand(this);
            StopSimCommand = new StopSimCommand(this);
            _logic.SetObserver(UpdateBalls);


        }

        public void StartSim()
        {
            IsSimulationOn = true;
            Trace.WriteLine("Simulation just started");
            _logic.SpawnBalls(BallsCount);
            _logic.Start();
        }

        public void StopSim()
        {
            IsSimulationOn = false;
            Trace.WriteLine("Simulation just ended");
            _logic.Stop();
        }

        public void UpdateBalls(IEnumerable<BallModel> ballModels = default)
        {
            if (ballModels is null) ballModels = new List<BallModel>();
            _balls = new ObservableCollection<BallModel>(ballModels);
            RaisePropertyChanged(nameof(Balls));
        }

    }
}