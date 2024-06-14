using Unity.Notifications.Android;
using System.Collections.Generic;
using Settings.Modules;
using Settings.Job;
using Tamagotchi;
using Settings;
using System;
using Core;

namespace Modules
{
    public sealed class PushNotificationsProvider : MonoBehaviourSingleton<PushNotificationsProvider>
    {
        private List<AndroidNotificationChannel> _channels;

        private PushNotificationsSettings _settings;
        private JobSettings _jobSettings;

        public void ScheduleNotifications()
        {
            ScheduleEnterTheGameNotification();
            ScheduleEndOfFullTimeJobNotification();
            ScheduleEndOfRecoveryPartTimeJobNotification();
        }

        public void CancelAllNotifications()
        {
            AndroidNotificationCenter.CancelAllNotifications();
        }

        private void ScheduleEnterTheGameNotification()
        {
            var channelId = PushNotificationsSettings.EnterTheGameChannelId;

            CreateNotification(channelId, PushNotificationsSettings.EnterTheGame12Id, DateTime.Now.AddHours(12));
            CreateNotification(channelId, PushNotificationsSettings.EnterTheGame24Id, DateTime.Now.AddHours(24));
        }

        private void ScheduleEndOfRecoveryPartTimeJobNotification()
        {
            if (Application.Model.PartTimeIsAvailable())
                return;

            var jobComponent = Application.Model.GetJobComponent();
            var date = jobComponent.StartPartTimeRecoveryDate.AddHours(_jobSettings.PartTimeRecoveryHours);

            CreateNotification(PushNotificationsSettings.JobChannelId, PushNotificationsSettings.EndOfRecoveryPartTimeJobId, date);
        }

        private void ScheduleEndOfFullTimeJobNotification()
        {
            var jobComponent = Application.Model.GetJobComponent();

            if (jobComponent.CurrentFullTimeJob == null)
                return;

            var date = jobComponent.CurrentFullTimeJob.StartDate.AddHours(jobComponent.CurrentFullTimeJob.WorkingHours);

            CreateNotification(PushNotificationsSettings.JobChannelId, PushNotificationsSettings.EndOfFullTimeJobId, date);
        }

        private void CreateChannel(string id, Importance importance)
        {
            var channel = new AndroidNotificationChannel()
            {
                Id = id,
                Importance = importance,
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

        private void Awake()
        {
            Instance = this;
            _settings = SettingsProvider.Get<PushNotificationsSettings>();
            _channels = new List<AndroidNotificationChannel>();
        }

        private void Start()
        {
            AndroidNotificationCenter.Initialize();
            AndroidNotificationCenter.CancelAllNotifications();

            CreateChannel(PushNotificationsSettings.JobChannelId, Importance.Default);
            CreateChannel(PushNotificationsSettings.EnterTheGameChannelId, Importance.High);
        }
    }
}