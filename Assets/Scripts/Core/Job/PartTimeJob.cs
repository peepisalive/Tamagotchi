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
    }
}