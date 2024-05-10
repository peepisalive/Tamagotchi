using UI.Modules.Navigation;
using Modules.Navigation;
using UnityEngine.UI;
using UnityEngine;
using Modules;
using UI.View;
using Events;
using Settings;
using Settings.Modules.Navigations;

namespace UI.Controller
{
    [RequireComponent(typeof(NavigationButtonView), typeof(NavigationElement))]
    public sealed class NavigationButtonController : MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private NavigationButtonView _view;
        [SerializeField] private NavigationElement _element;

        [Header("Other")]
        [SerializeField] private GameObject _transitionIcon;
        [SerializeField] private Toggle _toggle;

        public void Setup(NavigationPoint point, NavigationBlockType blockType)
        {
            var settings = SettingsProvider.Get<NavigationSettings>();
            var buttonData = point.Element.GetButtonData(point.Type);
            var stateIcon = settings.GetStateIcon(buttonData.StateType);

            _element.Setup(point, blockType);

            _view.SetIcon(buttonData.Icon);
            _view.SetTitle(buttonData.Title);
            _view.SetContent(buttonData.Description);
            _view.SetTransitionIcon(stateIcon);

            _toggle.gameObject.SetActive(buttonData.IsToggleButton);
            _transitionIcon.gameObject.SetActive(!buttonData.IsToggleButton);

            if (buttonData.IsToggleButton)
            {
                _toggle.isOn = buttonData.DefaultToggleState;
                SubscribeOnToggleUpdateStateEvent();
            }
        }

        private void UpdateToggleState(NavigationToggleUpdateStateEvent e)
        {
            if (e.Type != _element.NavigationPoint.Type)
                return;

            _toggle.isOn = e.State;
        }

        private void SubscribeOnToggleUpdateStateEvent()
        {
            EventSystem.Subscribe<NavigationToggleUpdateStateEvent>(UpdateToggleState);
        }

        private void UnsubscribeOnToggleUpdateStateEvent()
        {
            EventSystem.Unsubscribe<NavigationToggleUpdateStateEvent>(UpdateToggleState);
        }

        private void OnDestroy()
        {
            UnsubscribeOnToggleUpdateStateEvent();
        }
    }
}