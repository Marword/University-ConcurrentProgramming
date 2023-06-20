using BallSimulator.Data;
using BallSimulator.Data.API;
using BallSimulator.Data.LoggerFiles;
using BallSimulator.Logic.API;
using System.Diagnostics;
using Timer = System.Timers.Timer;
using System.Timers;


namespace BallSimulator.Logic;

internal class LogicApi : LogicAbstractApi
{

    public ICollection<IBall> _balls;
    private readonly ISet<IObserver<IBallLogic>> _observers;
    private readonly DataAbstractApi _data;
    private readonly Game _game;
    private readonly Random _rand = new();
    private readonly ILogger _logger;
    private Timer _timer;


    public LogicApi(DataAbstractApi? data = default, ILogger? logger = default)
    {
        _data = data ?? DataAbstractApi.CreateDataApi();
        _observers = new HashSet<IObserver<IBallLogic>>();

        _timer = new Timer(5000);
        _timer.Elapsed += TimerElapsed;
        _timer.Start();

        _logger = logger ?? new Logger();

        _game = new Game(_data.GameHeight, _data.GameWidth);
        _balls = new List<IBall>();
    }

    private void TimerElapsed(object e, ElapsedEventArgs args)
    {
        foreach (var ball in _balls)
        {
            _logger.LogInfo($"Ball Info: Diameter = {ball.Diameter}, Position = {ball.Coordinates}, Velocity = {ball.Tempo}");
        }
    }

    public override IEnumerable<IBall> MakeBalls(int count)
    {


        for (var i = 0; i < count; i++)
        {
            int diameter = GetRandomDiameter();
            Vector2 Coordinates = GetRandomPos(diameter);
            Vector2 Tempo = GetRandomTempo();
            var newBall = new Ball(diameter, Coordinates, Tempo);
            _balls.Add(newBall);
            TrackBall(new BallLogic(newBall));
        }
        ThreadManager.SetValidator(HandleCollisions);
        ThreadManager.Start();
        return _balls;

    }

    private Vector2 GetRandomPos(int diameter)
    {
        int radius = diameter / 2;
        int x = _rand.Next(radius, _game.Width - radius);
        int y = _rand.Next(radius, _game.Height - radius);

        return new Data.Vector2(x, y);

    }

    private Vector2 GetRandomTempo()
    {
        double x = (_rand.NextDouble() * 2.0 - 1.0) * _data.MaxTempo;
        double y = (_rand.NextDouble() * 2.0 - 1.0) * _data.MaxTempo;
        return new Vector2((float)x, (float)y);
    }

    private int GetRandomDiameter()
    {
        return _rand.Next(_data.MinDiameter, _data.MaxDiameter + 1);
    }

    private void HandleCollisions()
    {

        foreach (var (ball1, ball2) in Collisions.GetBallsCollisions(_balls))
        {
            (ball1.Tempo, ball2.Tempo, bool speedChanged) = Collisions.CalculateTempos(ball1, ball2);
        }

        foreach (var (ball, boundry, collisionsAxis) in Collisions.GetBoardCollisions(_balls, _game))
        {
            ball.Tempo = Collisions.CalculateTempo(ball, boundry, collisionsAxis);
        }

    }

    #region Provider

    public override IDisposable Subscribe(IObserver<IBallLogic> observer)
    {
        _observers.Add(observer);
        return new Unsubscriber(_observers, observer);
    }



    private void TrackBall(IBallLogic ball)
    {
        foreach (var observer in _observers)
        {
            observer.OnNext(ball);
        }
    }

    private void EndTransmisson()
    {
        foreach (var observer in _observers)
        {
            observer.OnCompleted();
        }
        _observers.Clear();
    }

    private class Unsubscriber : IDisposable
    {
        private readonly ISet<IObserver<IBallLogic>> _observers;
        private readonly IObserver<IBallLogic> _observer;

        public Unsubscriber(ISet<IObserver<IBallLogic>> observers, IObserver<IBallLogic> observer)
        {
            _observers = observers;
            _observer = observer;
        }


        public void Dispose()
        {
            _observers.Remove(_observer);
        }
    }

    #endregion



    public override void Dispose()
    {
        EndTransmisson();
        ThreadManager.Stop();

#if DEBUG
        Trace.WriteLine($"Average Delta = {ThreadManager.AverageDeltaTime}");
        Trace.WriteLine($"Average Fps = {ThreadManager.AverageFps}");
        Trace.WriteLine($"Total Frame Count = {ThreadManager.FrameCount}");
#endif
        _logger.LogInfo($"Average Delta = {ThreadManager.AverageDeltaTime}");
        _logger.LogInfo($"Average Fps = {ThreadManager.AverageFps}");
        _logger.LogInfo($"Total Frame Count = {ThreadManager.FrameCount}");

        foreach (var ball in _balls)
        {
            ball.Dispose();
        }
        _timer.Stop();
        _timer.Dispose();
        _balls.Clear();
        _logger.Dispose();
    }


}