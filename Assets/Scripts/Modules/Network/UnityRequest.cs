using UnityEngine.Networking;
using UnityEngine;
using System;

namespace Modules.Network
{
    public sealed class UnityRequest : Request
    {
        private UnityWebRequest _request;
        private UnityWebRequestAsyncOperation _operation;

        public void Initialize(UnityWebRequest request)
        {
            _request = request;
        }

        public override void Send()
        {
            if (InProcess)
                return;

            base.Send();

            if (_request == null)
                throw new NullReferenceException("request is null");

            if (_request.isModifiable)
                _operation = _request.SendWebRequest();

            if (_operation == null)
                throw new NullReferenceException("process is null");

            _operation.completed += OnOperationComplete;
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_operation != null)
                _operation.completed -= OnOperationComplete;

            _operation = null;

            _request?.Abort();
            _request?.Dispose();

            _request = null;
        }

        private void OnOperationComplete(AsyncOperation operation)
        {
            _operation.completed -= OnOperationComplete;
            _operation = null;

            Result = new RequestResult
            (
                _request.downloadHandler?.data,
                _request.downloadHandler?.text
            );

            if (_request.result == UnityWebRequest.Result.Success)
            {
                Complete();
            }
            else
            {
                Failed();
            }
        }
    }
}