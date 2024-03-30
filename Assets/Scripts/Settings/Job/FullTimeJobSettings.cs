using UnityEngine;
using Core.Job;

namespace Settings.Job
{
    [CreateAssetMenu(fileName = "FullTimeJobSettings", menuName = "Settings/Job/FullTimeJobSettings", order = 0)]
    public sealed class FullTimeJobSettings : JobTypeSettings
    {
        public override JobType Type => Core.Job.JobType.FullTime;

        [field: Header("Settings")]
        [field: SerializeField] public FullTimeJobType JobType { get; private set; }
        [field: SerializeField][field: Range(1, 24)] public int WorkingHours { get; private set; }
    }
}