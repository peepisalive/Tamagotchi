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

            EventSystem.Subscribe<Events.EndOfRecoveryPartTimeEvent>(MakePartTimeJobIsAvailable);
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
            EventSystem.Unsubscribe<Events.EndOfRecoveryPartTimeEvent>(MakePartTimeJobIsAvailable);
            EventSystem.Unsubscribe<Events.GettingJobEvent>(SendGettingJobEvent);
        }

        private void GetJob(Job job)
        {
            foreach (var i in _jobFilter)
            {
                ref var component = ref _jobFilter.Get1(i);

                if (job is PartTimeJob)
                {
                    ++component.PartTimeAmountPerDay;

                    if (!component.PartTimeIsAvailable())
                    {
                        component.StartPartTimeRecovery = DateTime.Now;

                        EventSystem.Send<Events.UpdateCurrentScreenEvent>();
                        InGameTimeManager.Instance.StartRecoveryCoroutine(_settings.PartTimeRecoveryHours * 3600f, () =>
                        {
                            EventSystem.Send<Events.EndOfRecoveryPartTimeEvent>();
                        });
                    }
                    else if (component.PartTimeAmountPerDay == _settings.PartTimeAmountPerDay - 1)
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

                var partTimeJobAmountPerDay = save.PartTimeJobAmountPerDay;
                var startPartTimeJobRecovery = save.StartPartTimeJobRecovery;
                var currentJob = (save.CurrentJob != null)
                    ? _factory.Create(save.CurrentJob) as FullTimeJob
                    : null;

                var currentTime = DateTime.Now;
                var endOfRecoveryPartTime = save.StartPartTimeJobRecovery + TimeSpan.FromHours(_settings.PartTimeRecoveryHours);

                if (endOfRecoveryPartTime > currentTime)
                {
                    StartRecoveryCoroutine<Events.EndOfRecoveryPartTimeEvent>(currentTime, endOfRecoveryPartTime);
                }
                else
                {
                    partTimeJobAmountPerDay = 0;
                    startPartTimeJobRecovery = default;
                }

                _world.NewEntity().Replace(new JobComponent
                {
                    AvailableJob = availableJob,
                    CurrentJob = currentJob,
                    PartTimeAmountPerDay = partTimeJobAmountPerDay,
                    StartPartTimeRecovery = startPartTimeJobRecovery,
                    StartFullTimeRecovery = save.StartFullTimeJobRecovery
                });
            }


            static void StartRecoveryCoroutine<T>(DateTime currentTime, DateTime endOfRecoveryPartTimeJob) where T : class, new()
            {
                var remainingSeconds = (endOfRecoveryPartTimeJob - currentTime).TotalSeconds;

                InGameTimeManager.Instance.StartRecoveryCoroutine((float)remainingSeconds, () =>
                {
                    EventSystem.Send(new T());
                });
            }
        }

        private void MakePartTimeJobIsAvailable(Events.EndOfRecoveryPartTimeEvent e)
        {
            foreach (var i in _jobFilter)
            {
                ref var component = ref _jobFilter.Get1(i);

                component.PartTimeAmountPerDay = 0;
                component.StartPartTimeRecovery = default;

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