using UI.Modules.Navigation;
using Modules.Navigation;
using UnityEngine;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(NavigationButtonView))]
    [RequireComponent(typeof(NavigationElement))]
    public sealed class NavigationButtonController : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private NavigationButtonView _view;
        [SerializeField] private NavigationElement _element;

        public void Setup(NavigationPoint point, NavigationBlockType blockType)
        {
            var buttonData = point.Element.GetButtonData(point.Type);

            _element.Setup(point, blockType);

            _view.SetIcon(buttonData.Icon);
            _view.SetTitle(buttonData.Title);
            _view.SetContent(buttonData.Description);
            _view.SetTransitionIcon(buttonData.StateType);
        }
    }
}