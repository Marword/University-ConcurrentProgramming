using Data.API;

namespace Logic.API;

public abstract class LogicAbstractApi : IObservable<IBallLogic>, IDisposable
{

    public abstract IEnumerable<IBall> MakeBalls(int count);
    public abstract IDisposable Subscribe(IObserver<IBallLogic> observer);
    public abstract void Dispose();
    public static LogicAbstractApi CreateLogicApi(DataAbstractApi? data = default, ILogger? logger = default)
    {
        return new LogicApi(data ?? DataAbstractApi.CreateDataApi(), logger);
    }
}