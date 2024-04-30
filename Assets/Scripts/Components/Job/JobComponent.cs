using System.Collections.Generic;
using Settings.Job;
using Core.Job;
using Settings;
using System;

namespace Components
{
    public struct JobComponent
    {
        public HashSet<Job> AvailableJob;
        public FullTimeJob CurrentJob;

        public int PartTimeJobAmountPerDay;

        public DateTime StartPartTimeJobRecovery;
        public DateTime StartFullTimeJobRecovery;

        public bool PartTimeJobIsAvailable()
        {
            return PartTimeJobAmountPerDay < SettingsProvider.Get<JobSettings>().PartTimeJobAmountPerDay;
        }
    }
}