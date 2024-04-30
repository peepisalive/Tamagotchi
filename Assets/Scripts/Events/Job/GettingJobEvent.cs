using Core.Job;

namespace Events
{
    public sealed class GettingJobEvent
    {
        public Job Job { get; private set; }
        public int WorkingHours { get; private set; }

        public GettingJobEvent(Job job, int workingHours = 0)
        {
            Job = job;
            WorkingHours = workingHours;
        }
    }
}