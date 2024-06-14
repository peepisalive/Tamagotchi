using System.Collections.Generic;
using System;

namespace Modules.Network
{
    public abstract class Request : INetRequest
    {
        public virtual RequestResult Result { get; protected set; }
        public virtual bool InProcess { get; private set; }
        public virtual bool IsCompleted { get; private set; }

        private readonly List<Action> _onStartCallbacks;
        private readonly List<IObserver<RequestResult>> _onCompletedObservers;
        private readonly List<IObserver<INetRequest>> _onErrorObservers;
        private readonly List<IObserver<bool>> _onFinishObservers;

        public Request()
        {
            _onStartCallbacks = new List<Action>();
            _onCompletedObservers = new List<IObserver<RequestResult>>();
            _onErrorObservers = new List<IObserver<INetRequest>>();
            _onFinishObservers = new List<IObserver<bool>>();
        }

        public virtual void Send()
        {
            if (InProcess)
                return;

            InProcess = true;
            IsCompleted = false;

            foreach (var callback in _onStartCallbacks)
            {
                callback?.Invoke();
            }
        }

        public IDisposable DoOnComplete(IObserver<RequestResult> observer)
        {
            _onCompletedObservers.Add(observer);
            return this;
        }

        public IDisposable DoOnError(IObserver<INetRequest> observer)
        {
            _onErrorObservers.Add(observer);
            return this;
        }

        public INetRequest DoOnFinish(IObserver<bool> observer)
        {
            _onFinishObservers.Add(observer);
            return this;
        }

        public INetRequest DoOnStart(Action callback)
        {
            _onStartCallbacks.Add(callback);
            return this;
        }

        public IDisposable Subscribe(IObserver<RequestResult> observer)
        {
            return DoOnComplete(observer);
        }

        public virtual void Dispose()
        {
            InProcess = false;
            IsCompleted = false;
            Result = default;

            _onStartCallbacks.Clear();

            ClearObserversList(_onCompletedObservers);
            ClearObserversList(_onFinishObservers);
            ClearObserversList(_onErrorObservers);


            void ClearObserversList<T>(List<IObserver<T>> observers)
            {
                foreach (var observer in observers)
                {
                    observer.OnCompleted();
                }

                observers.Clear();
            }
        }

        protected void Complete()
        {
            InProcess = false;
            IsCompleted = true;

            foreach (var observer in _onCompletedObservers)
            {
                observer.OnNext(Result);
            }

            foreach (var observer in _onFinishObservers)
            {
                observer.OnNext(true);
            }

            Dispose();
        }

        protected void Failed()
        {
            InProcess = false;
            IsCompleted = false;

            foreach (var observer in _onErrorObservers)
            {
                observer.OnNext(this);
            }

            foreach (var observer in _onFinishObservers)
            {
                observer.OnNext(true);
            }

            Dispose();
        }
    }
}