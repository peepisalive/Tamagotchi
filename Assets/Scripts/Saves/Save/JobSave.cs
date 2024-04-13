using Core.Job;
using System;

namespace Save
{
    [Serializable]
    public class JobSave
    {
        public int Salary;
    }


    [Serializable]
    public sealed class PartTimeJobSave : JobSave
    {
        public PartTimeJobType JobType;
    }


    [Serializable]
    public sealed class FullTimeJobSave : JobSave
    {
        public FullTimeJobType JobType;
        public int WorkingHours;
    }
}