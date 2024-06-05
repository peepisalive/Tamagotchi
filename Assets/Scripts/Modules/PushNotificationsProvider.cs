using Unity.Notifications.Android;
using System.Collections.Generic;
using Settings.Modules;
using Settings;
using Core;

namespace Modules
{
    public sealed class PushNotificationsProvider : MonoBehaviourSingleton<PushNotificationsProvider>
    {
        private Dictionary<AndroidNotificationChannel, List<AndroidNotification>> _notifications;
        private PushNotificationsSettings _settings;

        public void SchedulePetDeathNotification()
        {

        }

        public void ScheduleEnterTheGameNotification()
        {

        }

        private void Start()
        {
            AndroidNotificationCenter.CancelAllNotifications();

            Instance = this;
            _settings = SettingsProvider.Get<PushNotificationsSettings>();
        }

        private void OnDestroy()
        {
            if (_notifications == null)
                return;

            foreach (var notification in _notifications)
            {
                if (notification.Value == null)
                    continue;

                notification.Value.ForEach(n =>
                {
                    AndroidNotificationCenter.SendNotification(n, notification.Key.Id);
                });
            }
        }
    }
}