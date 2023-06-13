using Data.API;
using System.Diagnostics;

namespace Logic;

internal class LogicApi : LogicAbstractApi
{




    public IList<IBall> _balls;
    private readonly ISet<IObserver<IBall>> _observers;
    private readonly DataAbstractApi _data;
    private readonly Game _game;
    private readonly Random _rand = new();



    public LogicApi(DataAbstractApi? data = default)
    {
        _data = data ?? DataAbstractApi.CreateDataApi();
        _observers = new HashSet<IObserver<IBall>>();
        _game = new Game(_data.GameHeight, _data.GameWidth);
        _balls = new List<IBall>();
    }

    public override IEnumerable<IBall> MakeBalls(int count)
    {


        for (var i = 0; i < count; i++)
        {
            int diameter = GetRandomDiameter();
            Vector2 Coordinates = GetRandomPos(diameter);
            Vector2 Tempo = GetRandomTempo();
            Ball newBall = new(diameter, Coordinates, Tempo, _game);
            _balls.Add(newBall);
            TrackBall(newBall);
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

        return new Vector2(x, y);

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

    #region Provider



    public override IDisposable Subscribe(IObserver<IBall> observer)
    {
        _observers.Add(observer);
        return new Unsubscriber(_observers, observer);
    }



    private void TrackBall(IBall ball)
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

    private void HandleCollisions()
    {
        var collisions = Collisions.Get(_balls);
        if (collisions.Count > 0)
        {
            foreach (var col in collisions)
            {
                var (ball1, ball2) = col;
                (ball1.Tempo, ball2.Tempo) = Collisions.CalculateTempos(ball1, ball2);
            }
        }
        Thread.Sleep(1);
    }

    private class Unsubscriber : IDisposable
    {
        private readonly ISet<IObserver<IBall>> _observers;
        private readonly IObserver<IBall> _observer;

        public Unsubscriber(ISet<IObserver<IBall>> observers, IObserver<IBall> observer)
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

        Trace.WriteLine($"Average Delta = {ThreadManager.AverageDeltaTime}");
        Trace.WriteLine($"Average Fps = {ThreadManager.AverageFps}");
        Trace.WriteLine($"Total Frame Count = {ThreadManager.FrameCount}");

        foreach (var ball in _balls)
        {
            ball.Dispose();
        }
        _balls.Clear();
    }


}