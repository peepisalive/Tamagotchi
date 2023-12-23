using Application = Tamagotchi.Application;
using Modules.Navigation;
using UI.Screen.View;
using UnityEngine;

namespace UI.Screen.Controller
{
    [RequireComponent(typeof(ActionsScreenView))]
    public sealed class ActionsScreenController : ScreenController
    {
        [SerializeField] private ActionsScreenView _view;
        [SerializeField] private RectTransform _buttonsParent;

        private NavigationBlock _navigationBlock;
        private NavigationPoint _navigationPoint;

        public override void Setup()
        {
            base.Setup();
            
            if (_navigationBlock == null || _navigationPoint == null)
                return;

            foreach (var i in _navigationBlock.NavigationChain)
            {

            }
        }

        private void Awake()
        {
            _navigationBlock = Application.Model.GetCurrentNavigationBlock();
            _navigationPoint = Application.Model.GetCurrentNavigationPoint();
        }
    }
}