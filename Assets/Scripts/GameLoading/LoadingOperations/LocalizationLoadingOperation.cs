using UnityEngine.Localization.Settings;
using Localization;

namespace GameLoading.LoadingOperations
{
    public sealed class LocalizationLoadingOperation : LoadingOperation
    {
        public override float Progress => _progress;

        private float _progress;

        protected override void OnBegin()
        {
            LocalizationProvider.Initialize(LocalizationSettings.SelectedLocale).ContinueWith(_ =>
            {
                _progress = 1f;
                SetStateDone();

                UnityEngine.Debug.Log("pizda");
            });
        }
    }
}