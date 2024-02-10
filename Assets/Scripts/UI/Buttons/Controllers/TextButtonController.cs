using UnityEngine;
using UI.Settings;
using UI.View;

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

            _view.SetText(settings.Title);
        }
    }
}