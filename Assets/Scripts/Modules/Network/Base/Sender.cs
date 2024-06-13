namespace Modules.Network
{
    public abstract class Sender : INetSender
    {
        protected readonly string Token;
        protected readonly int RequestTimeout;

        public Sender(string token, int requestTimeout)
        {
            Token = token;
            RequestTimeout = requestTimeout;
        }

        public INetRequest Get(string url)
        {
            return GetRequest(new RequestSettings
            {
                Url = url,
                Type = RequestType.Get
            });
        }

        public INetRequest Delete(string url)
        {
            return GetRequest(new RequestSettings
            {
                Url = url,
                Type = RequestType.Delete
            });
        }

        public INetRequest Put(string url, object data)
        {
            return GetRequest(new RequestSettings
            {
                Url = url,
                Params = data,
                Type = RequestType.Put
            });
        }

        public INetRequest Post(string url, object data)
        {
            return GetRequest(new RequestSettings
            {
                Url = url,
                Params = data,
                Type = RequestType.Post
            });
        }

        protected abstract INetRequest GetRequest(RequestSettings settings);
    }
}