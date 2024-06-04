using Components.Modules.Navigation;
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
        private EcsFilter<EndOfFullTimeJobEvent> _recoveryFullTimeFilter;
        private EcsFilter<EndOfRecoveryPartTimeEvent> _recoveryPartTimeFilter;

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

            EventSystem.Subscribe<Events.EndOfRecoveryPartTimeEvent>(SendEndOfRecoveryPartTimeEvent);
            EventSystem.Subscribe<Events.EndOfFullTimeJobEvent>(SendEndOfFullTimeJobEvent);
            EventSystem.Subscribe<Events.GettingJobEvent>(SendGettingJobEvent);
        }

        public void Run()
        {
            if (!_gettingJobFilter.IsEmpty())
            {
                foreach (var i in _gettingJobFilter)
                {
                    var component = _gettingJobFilter.Get1(i);

                    GetJob(component.Job, component.WorkingHours);
                }
            }

            if (!_recoveryFullTimeFilter.IsEmpty())
                MakeFullTimeJobIsAvailable();

            if (!_recoveryPartTimeFilter.IsEmpty())
                MakePartTimeJobIsAvailable();
        }

        public void Destroy()
        {
            EventSystem.Unsubscribe<Events.EndOfRecoveryPartTimeEvent>(SendEndOfRecoveryPartTimeEvent);
            EventSystem.Unsubscribe<Events.EndOfFullTimeJobEvent>(SendEndOfFullTimeJobEvent);
            EventSystem.Unsubscribe<Events.GettingJobEvent>(SendGettingJobEvent);
        }

        private void GetJob(Job job, int workingHours)
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
                        StartRecoveryCoroutine<Events.EndOfRecoveryPartTimeEvent>(_settings.PartTimeRecoveryHours * 3600f);
                    }
                    else if (component.PartTimeAmountPerDay == _settings.PartTimeAmountPerDay - 1)
                    {
                        EventSystem.Send<Events.UpdateCurrentScreenEvent>();
                    }

                    _world.NewEntity().Replace(new ChangeBankAccountValueEvent
                    {
                        Value = job.Salary
                    });
                }
                else if (job is FullTimeJob fullTimeJob)
                {
                    component.CurrentFullTimeJob = new CurrentFullTimeJob(fullTimeJob, DateTime.Now, workingHours);

                    var remainingSeconds = workingHours * 3600f;

                    _world.NewEntity().Replace(new NavigationPointBackEvent());
                    StartRecoveryCoroutine<Events.EndOfFullTimeJobEvent>(remainingSeconds);
                    InGameTimeManager.Instance.StartCountRemainingTimeRoutine((int)remainingSeconds);
                }
            }


            static void StartRecoveryCoroutine<T>(float remainingSeconds) where T : class, new()
            {
                InGameTimeManager.Instance.StartRecoveryCoroutine(remainingSeconds, () =>
                {
                    EventSystem.Send(new T());
                });
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

                var currentFullTimeJob = (save.CurrentFullTimeJob != null)
                    ? new CurrentFullTimeJob
                    (
                        _factory.Create(save.CurrentFullTimeJob.JobSave) as FullTimeJob,
                        save.CurrentFullTimeJob.StartFullTimeJobRecovery,
                        save.CurrentFullTimeJob.WorkingHours
                    )
                    : null;

                _factory = new PartTimeJobFactory();

                foreach (var jobSave in save.PartTimeJob)
                {
                    availableJob.Add(_factory.Create(jobSave));
                }

                var partTimeJobAmountPerDay = save.PartTimeJobAmountPerDay;
                var startPartTimeJobRecovery = save.StartPartTimeJobRecovery;

                var currentTime = DateTime.Now;

                if (partTimeJobAmountPerDay >= _settings.PartTimeAmountPerDay)
                {
                    var endOfRecoveryPartTime = save.StartPartTimeJobRecovery + TimeSpan.FromHours(_settings.PartTimeRecoveryHours);
                    var remainingSeconds = (float)(endOfRecoveryPartTime - currentTime).TotalSeconds;

                    if (endOfRecoveryPartTime > currentTime)
                    {
                        StartRecoveryCoroutine<Events.EndOfRecoveryPartTimeEvent>(remainingSeconds);
                    }
                    else
                    {
                        partTimeJobAmountPerDay = 0;
                        startPartTimeJobRecovery = default;
                    }
                }

                if (currentFullTimeJob != null)
                {
                    var endOfRecoveryFullTime = currentFullTimeJob.StartFullTimeJobRecovery + TimeSpan.FromSeconds(currentFullTimeJob.WorkingHours * 3600f);
                    var remainingSeconds = (float)(endOfRecoveryFullTime - currentTime).TotalSeconds;

                    if (endOfRecoveryFullTime > currentTime)
                    {
                        StartRecoveryCoroutine<Events.EndOfFullTimeJobEvent>(remainingSeconds);
                        InGameTimeManager.Instance.StartCountRemainingTimeRoutine((int)remainingSeconds);
                    }
                    else
                    {
                        _world.NewEntity().Replace(new ChangeBankAccountValueEvent
                        {
                            Value = currentFullTimeJob.Job.Salary
                        });

                        currentFullTimeJob = null;
                    }
                }

                _world.NewEntity().Replace(new JobComponent
                {
                    AvailableJob = availableJob,
                    CurrentFullTimeJob = currentFullTimeJob,
                    PartTimeAmountPerDay = partTimeJobAmountPerDay,
                    StartPartTimeRecovery = startPartTimeJobRecovery,
                });
            }


            static void StartRecoveryCoroutine<T>(float remainingSeconds) where T : class, new()
            {
                InGameTimeManager.Instance.StartRecoveryCoroutine(remainingSeconds, () =>
                {
                    EventSystem.Send(new T());
                });
            }
        }

        private void MakePartTimeJobIsAvailable()
        {
            foreach (var i in _jobFilter)
            {
                ref var component = ref _jobFilter.Get1(i);

                component.PartTimeAmountPerDay = 0;
                component.StartPartTimeRecovery = default;
            }

            EventSystem.Send<Events.UpdateCurrentScreenEvent>();
        }

        private void MakeFullTimeJobIsAvailable()
        {
            foreach (var i in _jobFilter)
            {
                ref var component = ref _jobFilter.Get1(i);

                _world.NewEntity().Replace(new ChangeBankAccountValueEvent
                {
                    Value = component.CurrentFullTimeJob.Job.Salary
                });

                component.CurrentFullTimeJob = null;
            }

            EventSystem.Send(new Events.UpdateCurrentScreenEvent());
        }

        private void SendEndOfRecoveryPartTimeEvent(Events.EndOfRecoveryPartTimeEvent e)
        {
            _world.NewEntity().Replace(new EndOfRecoveryPartTimeEvent());
        }

        private void SendEndOfFullTimeJobEvent(Events.EndOfFullTimeJobEvent e)
        {
            _world.NewEntity().Replace(new EndOfFullTimeJobEvent());
        }

        private void SendGettingJobEvent(Events.GettingJobEvent e)
        {
            _world.NewEntity().Replace(new GettingJobEvent
            {
                Job = e.Job,
                WorkingHours = e.WorkingHours
            });
        }
    }
}