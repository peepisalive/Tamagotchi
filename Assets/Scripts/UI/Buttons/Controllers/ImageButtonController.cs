using UnityEngine;
using UI.Settings;
using UI.View;
using Utils;

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

            if (settings.MoneySignState)
                SetMoneyPrice(settings.MoneyValue);

            _view.SetIcon(settings.Icon);
        }
        public void SetMoneyPrice(int value)
        {
            _view.SetPrice((-value).ToMoneyString());
        }
    }
}