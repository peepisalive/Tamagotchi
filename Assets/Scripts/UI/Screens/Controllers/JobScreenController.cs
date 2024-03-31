using Application = Tamagotchi.Application;
using Modules.Navigation;
using UnityEngine;

namespace UI.Controller.Screen
{
    public sealed class JobScreenController : ScreenController
    {
        [Header("Controller")]
        [SerializeField] private RectTransform _buttonParent;

        private NavigationBlock _navigationBlock;
        private NavigationPoint _navigationPoint;

        public override void Setup()
        {
            base.Setup();

            if (_navigationBlock == null || _navigationPoint == null)
                return;

            var job = Application.Model.GetAvailableJob();
        }

        private void Awake()
        {
            _navigationBlock = Application.Model.GetCurrentNavigationBlock();
            _navigationPoint = Application.Model.GetCurrentNavigationPoint();
        }
    }
}