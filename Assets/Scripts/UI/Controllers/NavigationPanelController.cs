using Application = Tamagotchi.Application;
using Modules.Localization;
using UI.Controller;
using UnityEngine;
using UI.Settings;
using Modules;
using UI.View;
using Events;

namespace UI
{
    [RequireComponent(typeof(NavigationPanelView))]
    public sealed class NavigationPanelController : MonoBehaviour, IUpdatable<UpdateCurrentScreenEvent>
    {
        [SerializeField] private NavigationPanelView _view;

        [Header("Buttons")]
        [SerializeField] private ImageButtonController _backButton;
        [SerializeField] private ImageButtonController _homeButton;

        public void Setup()
        {
            _backButton?.Setup(new ImageButtonSettings
            {
                Action = () =>
                {
                    EventSystem.Send(new NavigationPointBackEvent());
                }
            });
            _homeButton?.Setup(new ImageButtonSettings
            {
                Action = () =>
                {
                    EventSystem.Send(new NavigationPointHomeEvent());
                }
            });

            UpdateState();
        }

        public void UpdateState(UpdateCurrentScreenEvent data = null)
        {

            var navigationPoint = Application.Model.GetCurrentNavigationPoint();
            var screenTitle = LocalizationProvider.GetNavigationText($"navigation_title_{navigationPoint.Type}");

            _view.SetText(screenTitle);
        }

        private void Start()
        {
            EventSystem.Subscribe<UpdateCurrentScreenEvent>(UpdateState);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<UpdateCurrentScreenEvent>(UpdateState);
        }
    }
}