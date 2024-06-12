namespace Modules.Network
{
    public struct RequestResult
    {
        public byte[] Data { get; private set; }
        public string Text { get; private set; }

        public RequestResult(byte[] data, string text)
        {
            Data = data;
            Text = text;
        }
    }
}