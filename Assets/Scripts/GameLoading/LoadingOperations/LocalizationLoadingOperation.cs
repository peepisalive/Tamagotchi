using UnityEngine.Localization.Settings;
using Modules.Localization;
using UnityEngine;
using System.Linq;

namespace GameLoading.LoadingOperations
{
    public sealed class LocalizationLoadingOperation : LoadingOperation
    {
        public override float Progress => _progress;

        private float _progress;

        protected override void OnBegin()
        {
            var localeCode = PlayerPrefs.GetString("selected-locale");
            var locale = !string.IsNullOrEmpty(localeCode)
                ? LocalizationSettings.AvailableLocales.Locales.First(locale => locale.Identifier.Code == localeCode)
                : LocalizationSettings.ProjectLocale;

            LocalizationProvider.Initialize(locale).ContinueWith(_ =>
            {
                _progress = 1f;
                SetStateDone();
            });
        }
    }
}