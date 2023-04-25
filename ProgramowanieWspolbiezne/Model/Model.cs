using Logic;

namespace Presentation.Model
{
    internal class Model : ModelApi
    {
        private readonly LogicAbstractApi _logic;
        public IEnumerable<BallModel> _ballModels => MapBallToBallModel();
        public Model(LogicAbstractApi logic = null)
        {
            _logic = logic ?? LogicAbstractApi.CreateLogicApi();
            _logic.SetObserver(NotifyUpdate);
        }
        public override void SpawnBalls(int count)
        {
            _logic.MakeBalls(count);
        }

        public override void Start()
        {
            _logic.StartSim();
        }

        public override void Stop()
        {
            _logic.StopSim();
        }

        public override void NotifyUpdate()
        {
            _observer.Invoke(_ballModels);
        }

        public override IEnumerable<BallModel> MapBallToBallModel()
        {
            List<BallModel> result = new List<BallModel>();
            foreach (Ball ball in _logic.Balls)
            {
                result.Add(new BallModel(ball));
            }
            return result;
        }
        public override void SetObserver(Observer observer)
        {
            _observer = observer;
        }
    }
}
