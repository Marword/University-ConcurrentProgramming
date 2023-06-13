using BallSimulator.Model;
using BallSimulator.Model.API;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BallSimulator.ViewModel
{
    public class SimViewModel : ViewModelBase, IObserver<IBallModel>
    {
        private ModelAbstractApi? _model;
        private readonly IChecker<int> _ballsCountChecker;
        private int _ballsCount = 8;
        private bool _isSimRunning = false;
        private IDisposable? unsubscriber;

        public int BallsCount
        {
            get => _ballsCount;
            set => SetField(ref _ballsCount, value, _ballsCountChecker, 1);

        }

        public bool IsSimRunning
        {
            get => _isSimRunning;
            private set => SetField(ref _isSimRunning, value);

        }

        public ObservableCollection<IBallModel> Balls { get; init; } = new();
        public ICommand StartSimCommand { get; init; }
        public ICommand StopSimCommand { get; init; }

        public SimViewModel(IChecker<int>? ballsCountChecker = default) : base()
        {
            _ballsCountChecker = ballsCountChecker ?? new BallsCountChecker();

            StartSimCommand = new StartSimCommand(this);
            StopSimCommand = new StopSimCommand(this);

        }

        public void StartSim()
        {
            _model = ModelAbstractApi.CreateModelApi();
            IsSimRunning = true;
            Follow(_model);
            _model.Start(BallsCount);
        }

        public void StopSim()
        {
            IsSimRunning = false;
            Balls.Clear();
            _model.Dispose();
        }

        #region Observer


        public void Follow(IObservable<IBallModel> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }

        public void OnCompleted()
        {
            unsubscriber?.Dispose();
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(IBallModel ball)
        {
            Balls.Add(ball);
        }



        #endregion
    }
}