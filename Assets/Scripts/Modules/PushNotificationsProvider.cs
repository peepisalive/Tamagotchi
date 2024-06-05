using Unity.Notifications.Android;
using System.Collections.Generic;
using Settings.Modules;
using Settings;
using System;
using Core;

namespace Modules
{
    public sealed class PushNotificationsProvider : MonoBehaviourSingleton<PushNotificationsProvider>
    {
        private Dictionary<string, List<AndroidNotification>> _notifications;
        private PushNotificationsSettings _settings;

        private void ScheduleEnterTheGameNotification()
        {
            var channelId = PushNotificationsSettings.EnterTheGameChannelId;

            CreateChannel(channelId);
            CreateNotification(channelId, PushNotificationsSettings.EnterTheGame12Id, DateTime.Now.Date.AddHours(12));
            CreateNotification(channelId, PushNotificationsSettings.EnterTheGame24Id, DateTime.Now.Date.AddHours(24));
        }

        private void CreateChannel(string id)
        {
            var channel = new AndroidNotificationChannel()
            {
                Id = id,
                Importance = Importance.Default,
                Name = _settings.Localization.GetChannelTitle(id),
                Description = _settings.Localization.GetChannelContent(id)
            };

            if (_notifications == null)
                _notifications = new Dictionary<string, List<AndroidNotification>>();

            _notifications.Add(channel.Id, new List<AndroidNotification>());
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }

        private void CreateNotification(string channelId, string id, DateTime date)
        {
            var notification = new AndroidNotification()
            {
                Title = _settings.Localization.GetNotificationTitle(id),
                Text = _settings.Localization.GetNotificationContent(id),
                FireTime = date,
                SmallIcon = "icon_0",
                LargeIcon = "icon_1",
                Style = NotificationStyle.BigTextStyle
            };

            if (_notifications.TryGetValue(channelId, out var notifications))
                notifications.Add(notification);
        }

        private void Start()
        {
            Instance = this;
#if UNITY_EDITOR
            return;
#endif
            _settings = SettingsProvider.Get<PushNotificationsSettings>();

            AndroidNotificationCenter.CancelAllNotifications();
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR
            return;
#endif
            ScheduleEnterTheGameNotification();

            foreach (var notification in _notifications)
            {
                if (notification.Value == null)
                    continue;

                notification.Value.ForEach(n =>
                {
                    AndroidNotificationCenter.SendNotification(n, notification.Key);
                });
            }
        }
    }
}