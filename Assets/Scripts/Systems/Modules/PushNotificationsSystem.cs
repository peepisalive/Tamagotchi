using Leopotam.Ecs;
using Components;
using Modules;
using System;

namespace Systems.Modules
{
    public sealed class PushNotificationsSystem : IEcsDestroySystem
    {
        private EcsFilter<JobComponent> _jobFilter;

        public void Destroy()
        {
#if UNITY_EDITOR
            return;
#endif
            foreach (var i in _jobFilter)
            {
                var currentFullTimeJob = _jobFilter.Get1(i).CurrentFullTimeJob;

                if (currentFullTimeJob == null)
                    break;

                var date = currentFullTimeJob.StartFullTimeJobRecovery + TimeSpan.FromSeconds(currentFullTimeJob.WorkingHours * 3600f);

                PushNotificationsProvider.Instance.ScheduleEndOfFullTimeJobNotification(date);
            }

            PushNotificationsProvider.Instance.ScheduleEnterTheGameNotification();
        }
    }
}