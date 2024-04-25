using System.Collections.Generic;
using Core.Job;

namespace Components
{
    public struct JobComponent
    {
        public HashSet<Job> AvailableJob;
        public FullTimeJob CurrentJob;

        public int PartTimeJobAmount;
    }
}