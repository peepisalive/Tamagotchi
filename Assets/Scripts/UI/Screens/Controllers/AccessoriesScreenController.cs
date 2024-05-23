using UnityEngine;

namespace UI.Controller.Screen
{
    public sealed class AccessoriesScreenController : ScreenController
    {
        [Header("Controller")]
        [SerializeField] private AccessoryChanger _accessoryChanger;

        public override void Setup()
        {
            base.Setup();
        }
    }
}