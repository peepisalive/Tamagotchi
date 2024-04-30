using System.Collections.Generic;
using Settings.Job;
using Leopotam.Ecs;
using System.Linq;
using Save.State;
using Components;
using Core.Job;
using Settings;
using Modules;
using System;

namespace Systems
{
    public sealed class JobSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter<JobComponent> _jobFilter;
        private EcsFilter<GettingJobEvent> _gettingJobFilter;
        private EcsFilter<SaveDataLoadedComponent> _saveDataFilter;

        private JobSettings _settings;
        private JobFactory _factory;

        public void Init()
        {
            _settings = SettingsProvider.Get<JobSettings>();

            if (_saveDataFilter.IsEmpty())
            {
                CreateJob();
            }
            else
            {
                LoadJob();
            }

            EventSystem.Subscribe<Events.EndOfRecoveryPartTimeJobEvent>(MakePartTimeJobIsAvailable);
            EventSystem.Subscribe<Events.GettingJobEvent>(SendGettingJobEvent);
        }

        public void Run()
        {
            if (!_gettingJobFilter.IsEmpty())
            {
                foreach (var i in _gettingJobFilter)
                {
                    GetJob(_gettingJobFilter.Get1(i).Job);
                }
            }
        }

        public void Destroy()
        {
            EventSystem.Unsubscribe<Events.EndOfRecoveryPartTimeJobEvent>(MakePartTimeJobIsAvailable);
            EventSystem.Unsubscribe<Events.GettingJobEvent>(SendGettingJobEvent);
        }

        private void GetJob(Job job)
        {
            foreach (var i in _jobFilter)
            {
                ref var component = ref _jobFilter.Get1(i);

                if (job is PartTimeJob)
                {
                    ++component.PartTimeJobAmountPerDay;

                    if (!component.PartTimeJobIsAvailable())
                    {
                        component.StartPartTimeJobRecovery = DateTime.Now;

                        EventSystem.Send<Events.UpdateCurrentScreenEvent>();
                        InGameTimeManager.Instance.StartRecoveryCoroutine(_settings.PartTimeJobAmountPerDay * 3600f, () =>
                        {
                            EventSystem.Send<Events.EndOfRecoveryPartTimeJobEvent>();
                        });
                    }
                    else if (component.PartTimeJobAmountPerDay == _settings.PartTimeJobAmountPerDay - 1)
                    {
                        EventSystem.Send<Events.UpdateCurrentScreenEvent>();
                    }
                }

                if (job is FullTimeJob fullTimeJob)
                {
                    component.AvailableJob.Remove(fullTimeJob);
                    component.CurrentJob = fullTimeJob;
                }
            }
        }

        private void CreateJob()
        {
            var availableJob = new HashSet<Job>();

            GetJob(JobType.FullTime);
            GetJob(JobType.PartTime);

            _world.NewEntity().Replace(new JobComponent
            {
                AvailableJob = availableJob
            });


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

                _world.NewEntity().Replace(new JobComponent
                {
                    AvailableJob = availableJob,
                    CurrentJob = currentJob,
                    PartTimeJobAmountPerDay = save.PartTimeJobAmountPerDay,
                    StartPartTimeJobRecovery = save.StartPartTimeJobRecovery,
                    StartFullTimeJobRecovery = save.StartFullTimeJobRecovery
                });
            }
        }

        private void MakePartTimeJobIsAvailable(Events.EndOfRecoveryPartTimeJobEvent e)
        {
            foreach (var i in _jobFilter)
            {
                ref var component = ref _jobFilter.Get1(i);

                component.PartTimeJobAmountPerDay = 0;
                component.StartPartTimeJobRecovery = default;

                EventSystem.Send<Events.UpdateCurrentScreenEvent>();
            }
        }

        private void SendGettingJobEvent(Events.GettingJobEvent e)
        {
            _world.NewEntity().Replace(new GettingJobEvent
            {
                Job = e.Job
            });
        }
    }
}