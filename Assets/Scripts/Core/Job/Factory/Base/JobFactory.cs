using Settings.Job;
using Save;

namespace Core.Job
{
    public abstract class JobFactory
    {
        public abstract Job Create(JobTypeSettings settings);
        public abstract Job Create(JobSave save);
    }
}