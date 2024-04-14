using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core.Job;

namespace Settings.Job
{
    [CreateAssetMenu(fileName = "JobSettings", menuName = "Settings/Job/JobSettings", order = 0)]
    public sealed class JobSettings : ScriptableObject
    {
        [field: SerializeField] public List<JobTypeSettings> JobTypeSettings { get; private set; }

        public FullTimeJobSettings GetFullTimeJobSettings(FullTimeJobType type)
        {
            var fullTimeJobSettings = JobTypeSettings.Where(s => s is FullTimeJobSettings).Select(s => s as FullTimeJobSettings);
            var settings = fullTimeJobSettings.FirstOrDefault(s => s.JobType == type);

            return settings;
        }

        public PartTimeJobSettings GetPartTimeJobSettings(PartTimeJobType type)
        {
            var fullTimeJobSettings = JobTypeSettings.Where(s => s is PartTimeJobSettings).Select(s => s as PartTimeJobSettings);
            var settings = fullTimeJobSettings.FirstOrDefault(s => s.JobType == type);

            return settings;
        }

#if UNITY_EDITOR
        public void Add(JobTypeSettings settings)
        {
            if (JobTypeSettings.Contains(settings))
                return;

            JobTypeSettings.Add(settings);
        }
    }
#endif
}