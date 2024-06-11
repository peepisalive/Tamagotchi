using UnityEngine.Localization.Settings;
using Modules.Localization;

namespace GameLoading.LoadingOperations
{
    public sealed class LocalizationLoadingOperation : LoadingOperation
    {
        public override float Progress => _progress;
        private float _progress;

        protected override async void OnBegin()
        {
            await LocalizationProvider.Initialize(LocalizationSettings.SelectedLocale).ContinueWith(_ =>
            {
                _progress = 1f;
                SetStateDone();
            });
        }
    }
}