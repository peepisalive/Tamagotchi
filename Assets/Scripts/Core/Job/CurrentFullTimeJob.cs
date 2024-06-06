using System;
using Save;

namespace Core.Job
{
    public sealed class CurrentFullTimeJob
    {
        public FullTimeJob Job { get; private set; }
        public int WorkingHours { get; private set; }
        public DateTime StartDate { get; private set; }

        public CurrentFullTimeJob(FullTimeJob job, DateTime startDate, int workingHours)
        {
            Job = job;
            WorkingHours = workingHours;
            StartDate = startDate;
        }

        public CurrentFullTimeJobSave GetSave()
        {
            return new CurrentFullTimeJobSave
            {
                JobSave = Job.GetSave() as FullTimeJobSave,
                WorkingHours = WorkingHours,
                StartDate = StartDate
            };
        }
    }
}