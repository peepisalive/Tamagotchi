using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;
using Extensions;
using System;

namespace Save.State
{
    public sealed class JobStateHolder : StateHolder<JobState>
    {
        public override string Id => "Job";
    }


    public sealed class JobState : IState
    {
        public List<FullTimeJobSave> FullTimeJob = new List<FullTimeJobSave>();
        public List<PartTimeJobSave> PartTimeJob = new List<PartTimeJobSave>();

        public CurrentFullTimeJobSave CurrentFullTimeJob;

        public DateTime StartPartTimeJobRecovery;
        public int PartTimeJobAmountPerDay;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
        }

        public async Task<string> ToStringAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var task = this.SerializeJsonAsync(Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            }, ct: ct);

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