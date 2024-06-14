using UnityEngine.Localization.Settings;
using GameLoading.LoadingOperations;

public sealed class LocalizationSettingsLoadingOperation : LoadingOperation
{
    public override float Progress => _progress;
    private float _progress;

    protected override async void OnBegin()
    {
        await LocalizationSettings.InitializationOperation.Task.ContinueWith(_ =>
        {
            _progress = 1;
            SetStateDone();
        });
    }
}
