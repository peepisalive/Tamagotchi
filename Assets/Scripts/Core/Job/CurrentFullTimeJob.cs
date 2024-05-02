using System;
using Save;

namespace Core.Job
{
    public sealed class CurrentFullTimeJob
    {
        public FullTimeJob Job { get; private set; }
        public int WorkingHours { get; private set; }
        public DateTime StartFullTimeJobRecovery { get; private set; }

        public CurrentFullTimeJob(FullTimeJob job, DateTime startFullTimeJobRecovery, int workingHours)
        {
            Job = job;
            WorkingHours = workingHours;
            StartFullTimeJobRecovery = startFullTimeJobRecovery;
        }

        public CurrentFullTimeJobSave GetSave()
        {
            return new CurrentFullTimeJobSave
            {
                JobSave = Job.GetSave() as FullTimeJobSave,
                WorkingHours = WorkingHours,
                StartFullTimeJobRecovery = StartFullTimeJobRecovery
            };
        }
    }
}