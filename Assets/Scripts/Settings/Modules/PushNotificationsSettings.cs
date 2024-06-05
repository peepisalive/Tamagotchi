using Modules.Localization;
using UnityEngine;
using System;

namespace Settings.Modules
{
    [CreateAssetMenu(fileName = "PushNotificationsSettings", menuName = "Settings/Modules/PushNotificationsSettings", order = 0)]
    public sealed class PushNotificationsSettings : ScriptableObject
    {
        [field: SerializeField] public PushNotificationsLocalization Localization { get; private set; }

        public const string EnterTheGameChannelId = "enter_the_game";
        public const string EnterTheGame12Id = "enter_the_game/12";
        public const string EnterTheGame24Id = "enter_the_game/24";

        public const string JobChannelId = "job";
        public const string EndOfFullTimeJobId = "end_of_full_time_job";

        [Serializable]
        public sealed class PushNotificationsLocalization
        {
            [SerializeField] private LocalizedText _asset;

            public string GetChannelTitle(string channelId)
            {
                return LocalizationProvider.GetText(_asset, $"{channelId}/channel/title");
            }

            public string GetChannelContent(string channelId)
            {
                return LocalizationProvider.GetText(_asset, $"{channelId}/channel/content");
            }

            public string GetNotificationTitle(string notificationId)
            {
                return LocalizationProvider.GetText(_asset, $"{notificationId}/notification/title");
            }

            public string GetNotificationContent(string notificationId)
            {
                return LocalizationProvider.GetText(_asset, $"{notificationId}/notification/content");
            }
        }
    }
}