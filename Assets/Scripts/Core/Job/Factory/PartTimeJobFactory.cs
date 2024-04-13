using Settings.Job;
using Save;

namespace Core.Job
{
    public sealed class PartTimeJobFactory : JobFactory
    {
        public override Job Create(JobTypeSettings settings)
        {
            var jobSettings = settings as PartTimeJobSettings;

            return new PartTimeJob(jobSettings.Salary, jobSettings.JobType);
        }

        public override Job Create(JobSave save)
        {
            var jobSave = save as PartTimeJobSave;

            return new PartTimeJob(jobSave.Salary, jobSave.JobType);
        }
    }
}