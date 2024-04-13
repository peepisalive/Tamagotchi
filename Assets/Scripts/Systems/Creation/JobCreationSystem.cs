using System.Collections.Generic;
using Settings.Job;
using Leopotam.Ecs;
using System.Linq;
using Save.State;
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
            var availableJob = new HashSet<Job>();

            GetJob(JobType.FullTime);
            GetJob(JobType.PartTime);

            SendJobComponent(availableJob);


            void GetJob(JobType type)
            {
                _factory = type == JobType.FullTime
                    ? new FullTimeJobFactory()
                    : new PartTimeJobFactory();

                foreach (var jobSettings in _settings.JobTypeSettings.Where(s => s.Type == type))
                {
                    availableJob.Add(_factory.Create(jobSettings));
                }
            }
        }

        private void LoadJob()
        {
            foreach (var i in _saveDataFilter)
            {
                var availableJob = new HashSet<Job>();
                var saveData = _saveDataFilter.Get1(i);
                var save = saveData.Get<JobStateHolder>().State;

                _factory = new FullTimeJobFactory();

                foreach (var jobSave in save.FullTimeJob)
                {
                    availableJob.Add(_factory.Create(jobSave));
                }

                _factory = new PartTimeJobFactory();

                foreach (var jobSave in save.PartTimeJob)
                {
                    availableJob.Add(_factory.Create(jobSave));
                }

                var currentJob = (save.CurrentJob != null)
                    ? _factory.Create(save.CurrentJob) as FullTimeJob
                    : null;

                SendJobComponent(availableJob, currentJob);
            }
        }

        private void SendJobComponent(HashSet<Job> availableJob, FullTimeJob currentJob = null)
        {
            _world.NewEntity().Replace(new JobComponent
            {
                AvailableJob = availableJob,
                CurrentJob = currentJob
            });
        }
    }
}