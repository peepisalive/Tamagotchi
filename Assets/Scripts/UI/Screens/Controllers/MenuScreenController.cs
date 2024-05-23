using Application = Tamagotchi.Application;
using System.Collections.Generic;
using Modules.Navigation;
using UnityEngine;
using Settings;
using Modules;
using Events;

namespace UI.Controller.Screen
{
    public sealed class MenuScreenController : ScreenController, IUpdatable<UpdateCurrentScreenEvent>
    {
        [Header("Controller")]
        [SerializeField] private RectTransform _buttonsParent;

        private NavigationBlock _navigationBlock;
        private NavigationPoint _navigationPoint;

        private List<NavigationButtonController> _buttons;

        public override void Setup()
        {
            base.Setup();

            if (_navigationBlock == null || _navigationPoint == null)
                return;

            UpdateState();
        }

        public void UpdateState(UpdateCurrentScreenEvent data = null)
        {
            if (_buttons != null)
            {
                _buttons.ForEach(button =>
                {
                    Destroy(button.gameObject);
                });
                _buttons.Clear();
            }
            else
            {
                _buttons = new List<NavigationButtonController>();
            }

            var points = Application.Model.GetChildPointsOfType(_navigationBlock.Type, _navigationPoint.Type);
            var buttonPrefab = SettingsProvider.Get<PrefabsSet>().NavigationButton;

            foreach (var point in points)
            {
                var button = Instantiate(buttonPrefab, _buttonsParent);

                button.Setup(point, _navigationBlock.Type);
                _buttons.Add(button);
            }
        }

        private void Awake()
        {
            _navigationBlock = Application.Model.GetCurrentNavigationBlock();
            _navigationPoint = Application.Model.GetCurrentNavigationPoint();

            EventSystem.Subscribe<UpdateCurrentScreenEvent>(UpdateState);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<UpdateCurrentScreenEvent>(UpdateState);
        }
    }
}