using UnityEngine;
using UI.Settings;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(ImageButtonView))]
    public sealed class ImageButtonController : ButtonController
    {
        [Header("Controller")]
        [SerializeField] private ImageButtonView _view;

        public void Setup(ImageButtonSettings settings)
        {
            base.Setup(settings);

            _view.SetIcon(settings.Icon);
            _view.SetAdsSignState(settings.AdsSignState);
        }
    }
}