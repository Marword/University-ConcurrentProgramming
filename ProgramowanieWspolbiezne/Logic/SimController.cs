using Data;

namespace Logic
{
    internal class SimController : LogicAbstractApi
    {
        private readonly DataAbstractApi _data;
        private readonly BallManager _ballManager;
        public override Ball[] Balls => _ballManager.Balls;

        private bool _running;

        public SimController(DataAbstractApi data = default)
        {
            _data = data ?? DataAbstractApi.CreateDataApi();
            _ballManager = new BallManager(new Game(_data.GameHeight, _data.GameWidth), _data.BallRadius);

        }



        public override void MakeBalls(int count)
        {
            _ballManager.BallCreationRandomCoord(count);
        }

        public override void StartSim()
        {
            if (!_running)
            {
                _running = true;
                Task.Run(InvSim);
            }
        }

        public override void InvSim()
        {
            while (_running)
            {
                _ballManager.PushAllBalls();
                NotifyUpdate();
                Thread.Sleep(10);
            }
        }

        public override void StopSim()
        {
            if (_running) _running = false;
        }

        public override void NotifyUpdate()
        {
            _observer.Invoke();
        }

        public override void SetObserver(Observer observer)
        {
            _observer = observer;
        }
    }
}
