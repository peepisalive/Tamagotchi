namespace Core.Job
{
    public sealed class FullTimeJob : Job
    {
        public override JobType Type => Core.Job.JobType.FullTime;

        public FullTimeJobType JobType { get; private set; }
        public int WorkingHours { get; private set; }

        public FullTimeJob(int salary, FullTimeJobType jobType, int workingHours) : base(salary)
        {
            JobType = jobType;
            WorkingHours = workingHours;
        }
    }
}