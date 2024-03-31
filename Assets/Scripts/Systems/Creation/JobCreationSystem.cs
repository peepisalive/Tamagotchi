using System.Collections.Generic;
using Settings.Job;
using Leopotam.Ecs;
using System.Linq;
using Components;
using Core.Job;
using Settings;

namespace Systems.Creation
{
    public sealed class JobCreationSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<SaveDataLoadedComponent> _saveDataFilter;

        private JobSettings _settings;
        private JobFactory _factory;

        public void Init()
        {
            if (_saveDataFilter.IsEmpty())
            {
                CreateJob();
            }
            else
            {
                LoadJob();
            }
        }

        private void CreateJob()
        {
            _settings = SettingsProvider.Get<JobSettings>();
            _factory = new FullTimeJobFactory();

            var availableJob = new HashSet<Job>();

            foreach (var jobSettings in _settings.JobTypeSettings.Where(s => s.Type == JobType.FullTime))
            {
                availableJob.Add(_factory.Create(jobSettings));
            }

            _factory = new PartTimeJobFactory();

            foreach (var jobSettings in _settings.JobTypeSettings.Where(s => s.Type == JobType.PartTime))
            {
                availableJob.Add(_factory.Create(jobSettings));
            }

            _world.NewEntity().Replace(new JobComponent
            {
                AvailableJob = availableJob
            });
        }

        private void LoadJob()
        {

        }
    }
}