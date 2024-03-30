using Settings.Job;

namespace Core.Job
{
    public sealed class PartTimeJobFactory : JobFactory
    {
        public override Job Create(JobTypeSettings settings)
        {
            var jobSettings = settings as PartTimeJobSettings;

            return new PartTimeJob(jobSettings.Salary, jobSettings.JobType);
        }
    }
}