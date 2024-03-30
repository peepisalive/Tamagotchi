namespace Core.Job
{
    public abstract class Job
    {
        public abstract JobType Type { get; }
        public int Salary { get; private set; }

        public Job(int salary)
        {
            Salary = salary;
        }
    }
}