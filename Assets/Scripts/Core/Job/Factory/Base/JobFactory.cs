using Settings.Job;

namespace Core.Job
{
    public abstract class JobFactory
    {
        public abstract Job Create(JobTypeSettings settings);
    }
}