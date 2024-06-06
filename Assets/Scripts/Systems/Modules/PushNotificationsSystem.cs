using Leopotam.Ecs;
using Settings.Job;
using Components;
using Settings;
using Modules;

namespace Systems.Modules
{
    public sealed class PushNotificationsSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private EcsFilter<JobComponent> _jobFilter;
        private JobSettings _jobSettings;

        public void Init()
        {
            _jobSettings = SettingsProvider.Get<JobSettings>();
        }

        public void Destroy()
        {
#if UNITY_EDITOR
            return;
#endif
            foreach (var i in _jobFilter)
            {
                var component = _jobFilter.Get1(i);
                var currentFullTimeJob = component.CurrentFullTimeJob;

                if (currentFullTimeJob != null)
                {
                    var date = currentFullTimeJob.StartDate.AddHours(currentFullTimeJob.WorkingHours);

                    PushNotificationsProvider.Instance.ScheduleEndOfFullTimeJobNotification(date);
                }

                if (component.PartTimeIsAvailable())
                    break;

                var partTimeEndOfRecoveryDate = component.StartPartTimeRecoveryDate.AddHours(_jobSettings.PartTimeRecoveryHours);

                PushNotificationsProvider.Instance.ScheduleEndOfRecoveryPartTimeJobNotification(partTimeEndOfRecoveryDate);
            }

            PushNotificationsProvider.Instance.ScheduleEnterTheGameNotification();
        }
    }
}