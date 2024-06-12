using System;

namespace Modules.Network
{
    public interface INetRequest : IObservable<RequestResult>, IDisposable
    {
        public bool InProcess { get; }
        public bool IsCompleted { get; }
        public RequestResult Result { get; }

        public void Send();

        public INetRequest DoOnStart(Action callback);
        public IDisposable DoOnComplete(IObserver<RequestResult> observer);
        public IDisposable DoOnError(IObserver<INetRequest> observer);
        public INetRequest DoOnFinish(IObserver<bool> observer);
    }
}