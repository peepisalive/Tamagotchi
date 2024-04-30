using Core.Job;

namespace Events
{
    public sealed class GettingJobEvent
    {
        public Job Job { get; private set; }

        public GettingJobEvent(Job job)
        {
            Job = job;
        }
    }
}