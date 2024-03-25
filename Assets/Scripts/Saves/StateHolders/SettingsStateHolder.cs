using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;
using Extensions;
using System;

namespace Save.State
{
    public sealed class SettingsStateHolder : StateHolder<SettingsState>
    {
        public override string Id => "Settings";
    }


    public sealed class SettingsState : IState
    {
        public bool SoundState = true;

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