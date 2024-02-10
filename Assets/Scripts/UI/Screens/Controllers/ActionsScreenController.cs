using Application = Tamagotchi.Application;
using Modules.Navigation;
using UI.View.Screen;
using UnityEngine;

namespace UI.Controller.Screen
{
    [RequireComponent(typeof(ActionsScreenView))]
    public sealed class ActionsScreenController : ScreenController
    {
        [Header("View")]
        [SerializeField] private ActionsScreenView _view;
        [SerializeField] private RectTransform _buttonsParent;
        [SerializeField] private NavigationButtonController _buttonPrefab;

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
                Instantiate(_buttonPrefab, _buttonsParent)
                    .Setup(point, _navigationBlock.Type);
            }
        }

        private void Awake()
        {
            _navigationBlock = Application.Model.GetCurrentNavigationBlock();
            _navigationPoint = Application.Model.GetCurrentNavigationPoint();
        }
    }
}