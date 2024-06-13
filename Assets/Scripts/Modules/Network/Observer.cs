using System;

namespace Modules.Network
{
    public sealed class Observer<T> : IObserver<T>
    {
        private Action<T> _callback;

        public Observer(Action<T> callback)
        {
            _callback = callback;
        }

        public void OnCompleted()
        {
            _callback = null;
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(T value)
        {
            _callback?.Invoke(value);
        }
    }
}