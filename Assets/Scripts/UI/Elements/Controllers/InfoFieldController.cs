using UnityEngine;
using UI.Settings;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(InfoFieldView))]
    public sealed class InfoFieldController : MonoBehaviour
    {
        [SerializeField] private InfoFieldView _view;

        public void Setup(InfoFieldSettings settings)
        {
            _view.SetTitle(settings.Title);
            _view.SetContent(settings.Content);
            _view.SetIcon(settings.Icon, settings.IconState);
        }
    }
}