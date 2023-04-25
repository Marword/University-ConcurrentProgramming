using Data;



namespace Logic
{
    public abstract class LogicAbstractApi
    {
        protected Observer _observer;
        public abstract Ball[] Balls { get; }

        public delegate void Observer();
        public abstract void NotifyUpdate();
        public abstract void SetObserver(Observer observer);

        public abstract void MakeBalls(int count);
        public abstract void InvSim();
        public abstract void StartSim();
        public abstract void StopSim();

        public static LogicAbstractApi CreateLogicApi(DataAbstractApi data = default)
        {
            return new SimController(data ?? DataAbstractApi.CreateDataApi());
        }
    }
}