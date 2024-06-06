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
        private List<AndroidNotificationChannel> _channels;
        private PushNotificationsSettings _settings;

        public void ScheduleEndOfRecoveryPartTimeJobNotification(DateTime date)
        {
            CreateNotification(PushNotificationsSettings.JobChannelId, PushNotificationsSettings.EndOfRecoveryPartTimeJobId, date);
        }

        public void ScheduleEndOfFullTimeJobNotification(DateTime date)
        {
            CreateNotification(PushNotificationsSettings.JobChannelId, PushNotificationsSettings.EndOfFullTimeJobId, date);
        }

        public void ScheduleEnterTheGameNotification()
        {
            var channelId = PushNotificationsSettings.EnterTheGameChannelId;

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
#if UNITY_EDITOR
            return;
#endif
            AndroidNotificationCenter.CancelAllNotifications();

            CreateChannel(PushNotificationsSettings.JobChannelId);
            CreateChannel(PushNotificationsSettings.EnterTheGameChannelId);

        }

        private void Awake()
        {
            Instance = this;
            _settings = SettingsProvider.Get<PushNotificationsSettings>();
            _channels = new List<AndroidNotificationChannel>();
        }
    }
}