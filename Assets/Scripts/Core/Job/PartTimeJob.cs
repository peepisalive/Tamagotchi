using Save;

namespace Core.Job
{
    public sealed class PartTimeJob : Job
    {
        public override JobType Type => Core.Job.JobType.PartTime;

        public PartTimeJobType JobType { get; private set; }

        public PartTimeJob(int salary, PartTimeJobType jobType) : base(salary)
        {
            JobType = jobType;
        }

        public override JobSave GetSave()
        {
            return new PartTimeJobSave
            {
                Salary = Salary,
                JobType = JobType
            };
        }
    }
}