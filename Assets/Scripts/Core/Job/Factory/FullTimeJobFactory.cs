using Settings.Job;

namespace Core.Job
{
    public sealed class FullTimeJobFactory : JobFactory
    {
        public override Job Create(JobTypeSettings settings)
        {
            var jobSettings = settings as FullTimeJobSettings;

            return new FullTimeJob(jobSettings.Salary, jobSettings.JobType, jobSettings.WorkingHours);
        }
    }
}