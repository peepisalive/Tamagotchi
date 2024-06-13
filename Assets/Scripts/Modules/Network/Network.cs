namespace Modules.Network
{
    public static class Network
    {
        private const string BASE_URL = "";
        private const string TOKEN = "";

        private static INetSender _sender;

        static Network()
        {
            _sender = new UnitySender();
        }

        private static INetRequest Get(string id = null)
        {
            var url = BASE_URL;

            if (!string.IsNullOrEmpty(id))
                url = BASE_URL + $"/{id}";

            return _sender.Get(url);
        }

        private static INetRequest Put(string url, object data)
        {
            return _sender.Put(url, data);
        }

        private static INetRequest Post(string url, object data)
        {
            return _sender.Post(url, data);
        }

        private static INetRequest Delete(string id)
        {
            return _sender.Delete(BASE_URL + $"/{id}");
        }
    }
}