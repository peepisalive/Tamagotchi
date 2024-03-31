using Application = Tamagotchi.Application;
using Modules.Navigation;
using UnityEngine;

namespace UI.Controller.Screen
{
    public sealed class ActionsScreenController : ScreenController
    {
        [Header("Controller")]
        [SerializeField] private NavigationButtonController _buttonPrefab;
        [SerializeField] private RectTransform _buttonsParent;

        private NavigationBlock _navigationBlock;
        private NavigationPoint _navigationPoint;

        public override void Setup()
        {
            base.Setup();
            
            if (_navigationBlock == null || _navigationPoint == null)
                return;

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