namespace Modules.Network
{
    public interface INetSender
    {
        public INetRequest Get(string url);
        public INetRequest Delete(string url);
        public INetRequest Put(string url, object data);
        public INetRequest Post(string url, object data);
    }
}