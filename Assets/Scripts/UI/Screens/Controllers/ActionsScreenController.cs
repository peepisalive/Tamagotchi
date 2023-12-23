using Application = Tamagotchi.Application;
using Modules.Navigation;
using UI.Screen.View;
using UI.Controller;
using UnityEngine;

namespace UI.Screen.Controller
{
    [RequireComponent(typeof(ActionsScreenView))]
    public sealed class ActionsScreenController : ScreenController
    {
        [Header("View")]
        [SerializeField] private ActionsScreenView _view;
        [SerializeField] private NavigationButtonController _buttonPrefab;
        [SerializeField] private RectTransform _buttonsParent;

        private NavigationBlock _navigationBlock;
        private NavigationPoint _navigationPoint;

        public override void Setup()
        {
            base.Setup();
            
            if (_navigationBlock == null || _navigationPoint == null)
                return;

            foreach (var navigationPoint in _navigationBlock.NavigationChain)
            {
                if (navigationPoint.Type == _navigationPoint.Type)
                    _navigationPoint = navigationPoint;
            }

            var points = Application.Model.GetChildPointsOfType(_navigationBlock.Type, _navigationPoint.Type);

            foreach (var point in points)
            {
                Instantiate(_buttonPrefab, _buttonsParent);
            }
        }

        private void Awake()
        {
            _navigationBlock = Application.Model.GetCurrentNavigationBlock();
            _navigationPoint = Application.Model.GetCurrentNavigationPoint();
        }
    }
}