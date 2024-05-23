using UnityEngine;
using UI.Settings;
using UI.View;
using Utils;

namespace UI.Controller
{
    [RequireComponent(typeof(TextButtonView))]
    public sealed class TextButtonController : ButtonController
    {
        [Header("Controller")]
        [SerializeField] private TextButtonView _view;

        public void Setup(TextButtonSettings settings)
        {
            base.Setup(settings);

            if (settings.MoneySignState)
                SetMoneyPrice(settings.MoneyValue);

            _view.SetText(settings.Title);
        }

        public void SetMoneyPrice(int value)
        {
            _view.SetPrice((-value).ToMoneyString());
        }
    }
}