using UnityEngine;
using Core.Job;

namespace Settings.Job
{
    [CreateAssetMenu(fileName = "PartTimeJobSettings", menuName = "Settings/Job/PartTimeJobSettings", order = 0)]
    public sealed class PartTimeJobSettings : JobTypeSettings
    {
        public override JobType Type => Core.Job.JobType.PartTime;

        [field: Header("Settings")]
        [field: SerializeField] public PartTimeJobType JobType { get; private set; }
    }
}