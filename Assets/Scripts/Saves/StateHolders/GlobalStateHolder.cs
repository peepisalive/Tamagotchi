using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.Navigation;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;
using Extensions;
using System;

namespace Save.State
{
    public sealed class GlobalStateHolder : StateHolder<GlobalState>
    {
        public override string Id => "Global";
    }


    public sealed class GlobalState : IState
    {
        public int BankAccountValue;
        public float PlayTimeSeconds;

        public HashSet<NavigationElementType> NavigationTracks = new HashSet<NavigationElementType>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
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