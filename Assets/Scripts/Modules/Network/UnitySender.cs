using UnityEngine.Networking;
using System.Text;

namespace Modules.Network
{
    public sealed class UnitySender : Sender
    {
        public UnitySender(int requestTimeout = 35) : base(requestTimeout) { }

        protected override INetRequest GetRequest(RequestSettings settings)
        {
            var request = (UnityWebRequest)default;

            switch (settings.Type)
            {
                case RequestType.Get:
                {
                    request = UnityWebRequest.Get(settings.Url);
                    break;
                }
                case RequestType.Put:
                {
                    request = new UnityWebRequest(settings.Url, "PUT");

                    if (settings.Params is not string body)
                        break;

                    SetBody(request, body);
                        
                    break;
                }
                case RequestType.Post:
                {
                    request = new UnityWebRequest(settings.Url, "POST");

                    if (settings.Params is not string body)
                        break;

                    SetBody(request, body);
                        
                    break;
                }
                case RequestType.Delete:
                {
                    request = UnityWebRequest.Delete(settings.Url);
                    break;
                }
            }

            request.timeout = RequestTimeout;

            if (!string.IsNullOrEmpty(settings.Token))
                request.SetRequestHeader("Token", settings.Token);

            var unityRequest = new UnityRequest();

            unityRequest.Initialize(request);
            unityRequest.Send();

            return unityRequest;
        }

        private UnityWebRequest SetBody(UnityWebRequest request, string body)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(body);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");

            return request;
        }
    }
}