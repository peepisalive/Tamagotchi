using Settings.Job;
using Save;

namespace Core.Job
{
    public sealed class FullTimeJobFactory : JobFactory
    {
        public override Job Create(JobTypeSettings settings)
        {
            var jobSettings = settings as FullTimeJobSettings;

            return new FullTimeJob(jobSettings.Salary, jobSettings.JobType, jobSettings.WorkingHours);
        }

        public override Job Create(JobSave save)
        {
            var jobSave = save as FullTimeJobSave;

            return new FullTimeJob(jobSave.Salary, jobSave.JobType, jobSave.WorkingHours);
        }
    }
}