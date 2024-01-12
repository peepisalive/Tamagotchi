using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;
using Extensions;
using System;
using Core;

namespace Save.State
{
    public sealed class PetStateHolder : StateHolder<PetState>
    {
        public override string Id => "Pet";
    }


    public sealed class PetState : IState
    {
        public string Id;
        public string Name;
        public PetType Type;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public async Task<string> ToStringAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var task = this.SerializeJsonAsync(ct: ct);

            try
            {
                await task;
            }
            catch (OperationCanceledException e)
            {
#if UNITY_EDITOR
                Debug.LogError($"{GetType()}: ToStringAsync cancelled. Message: {e.Message}");
#endif
                return string.Empty;
            }

            return task.Result;
        }
    }
}