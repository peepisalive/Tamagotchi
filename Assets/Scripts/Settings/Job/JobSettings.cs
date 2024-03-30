using System.Collections.Generic;
using UnityEngine;

namespace Settings.Job
{
    [CreateAssetMenu(fileName = "JobSettings", menuName = "Settings/Job/JobSettings", order = 0)]
    public sealed class JobSettings : ScriptableObject
    {
        [field: SerializeField] public List<JobTypeSettings> JobTypeSettings { get; private set; }

        public void Add(JobTypeSettings settings)
        {
            if (JobTypeSettings.Contains(settings))
                return;

            JobTypeSettings.Add(settings);
        }
    }
}