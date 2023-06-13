namespace BallSimulator.Data.API;

public interface IBall : IObservable<IBall>, IDisposable
{
    int Diameter { get; }
    int Radius { get; }
    Vector2 Tempo { get; set; }
    Vector2 Coordinates { get; }

    void Move(float scaler);
    bool Touches(IBall ball);
}