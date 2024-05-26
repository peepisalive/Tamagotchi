using Modules.Localization;

namespace GameLoading.LoadingOperations
{
    public sealed class LocalizationLoadingOperation : LoadingOperation
    {
        public override float Progress => _progress;

        private float _progress;

        protected override void OnBegin()
        {
            LocalizationProvider.Initialize().ContinueWith(_ =>
            {
                _progress = 1f;
                SetStateDone();
            });
        }
    }
}