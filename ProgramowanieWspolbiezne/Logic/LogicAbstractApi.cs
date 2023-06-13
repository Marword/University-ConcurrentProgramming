using Data.API;

namespace Logic
{
    public abstract class LogicAbstractApi : IObservable<IBall>, IDisposable
    {
        public abstract IEnumerable<IBall> MakeBalls(int count);
        public abstract IDisposable Subscribe(IObserver<IBall> observer);
        public abstract void Dispose();

        public static LogicAbstractApi CreateLogicApi(DataAbstractApi data = default)
        {
            return new LogicApi(data ?? DataAbstractApi.CreateDataApi());
        }
    }
}