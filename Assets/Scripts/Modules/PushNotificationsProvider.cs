using Unity.Notifications.Android;
using System.Collections.Generic;
using Settings.Modules;
using System.Linq;
using Settings;
using System;
using Core;

namespace Modules
{
    public sealed class PushNotificationsProvider : MonoBehaviourSingleton<PushNotificationsProvider>
    {
        private List<AndroidNotificationChannel> _channels;
        private PushNotificationsSettings _settings;

        public void ScheduleEndOfFullTimeJobNotification(DateTime date)
        {
            var channelId = PushNotificationsSettings.JobChannelId;

            CreateChannel(channelId);
            CreateNotification(channelId, PushNotificationsSettings.EndOfFullTimeJobId, date);
        }

        public void ScheduleEnterTheGameNotification()
        {
            var channelId = PushNotificationsSettings.EnterTheGameChannelId;

            CreateChannel(channelId);
            CreateNotification(channelId, PushNotificationsSettings.EnterTheGame12Id, DateTime.Now.Date.AddHours(12));
            CreateNotification(channelId, PushNotificationsSettings.EnterTheGame24Id, DateTime.Now.Date.AddHours(24));
        }

        private void CreateChannel(string id)
        {
            if (_channels.Any(c => c.Id == id))
                return;

            var channel = new AndroidNotificationChannel()
            {
                Id = id,
                Importance = Importance.Default,
                Name = _settings.Localization.GetChannelTitle(id),
                Description = _settings.Localization.GetChannelContent(id)
            };

            _channels.Add(channel);
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

            AndroidNotificationCenter.SendNotification(notification, channelId);
        }

        private void Start()
        {
            Instance = this;
#if UNITY_EDITOR
            return;
#endif
            _settings = SettingsProvider.Get<PushNotificationsSettings>();
            _channels = new List<AndroidNotificationChannel>();

            AndroidNotificationCenter.CancelAllNotifications();
        }
    }
}