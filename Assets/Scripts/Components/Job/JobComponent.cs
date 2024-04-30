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

        public int PartTimeAmountPerDay;

        public DateTime StartPartTimeRecovery;
        public DateTime StartFullTimeRecovery;

        public bool PartTimeIsAvailable()
        {
            return PartTimeAmountPerDay < SettingsProvider.Get<JobSettings>().PartTimeAmountPerDay;
        }
    }
}