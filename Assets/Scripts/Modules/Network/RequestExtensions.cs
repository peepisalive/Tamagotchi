using System;

namespace Modules.Network.Extensions
{
    public static class RequestExtensions
    {
        public static INetRequest OnStart(this INetRequest request, Action callback)
        {
            return request.DoOnStart(callback);
        }

        public static INetRequest OnFinish(this INetRequest request, Action<bool> callback)
        {
            var observer = new Observer<bool>(_ => callback?.Invoke(!request.InProcess));
            return request.DoOnFinish(observer);
        }

        public static INetRequest OnComplete(this INetRequest request, Action<RequestResult> callback)
        {
            var observer = new Observer<RequestResult>(_ => callback?.Invoke(new RequestResult
            (
                 request.Result.Data,
                 request.Result.Text
            )));
            request.DoOnComplete(observer);

            return request;
        }

        public static INetRequest OnError(this INetRequest request, Action<INetRequest> callback)
        {
            var observer = new Observer<INetRequest>(_ => callback?.Invoke(request));
            request.DoOnError(observer);

            return request;
        }
    }
}