﻿using Logic;

namespace Model
{

    public abstract class ModelAbstractApi : IObserver<IBall>, IObservable<IBallModel>
    {
        public abstract void Start(int count);
        public abstract void Stop();
        public abstract void OnCompleted();
        public virtual void OnError(Exception error) => throw error;
        public abstract void OnNext(IBall value);
        public abstract IDisposable Subscribe(IObserver<IBallModel> observer);

        public static ModelAbstractApi CreateModelApi(LogicAbstractApi? logic = default)
        {
            return new ModelApi(logic ?? LogicAbstractApi.CreateLogicApi());
        }

    }
}