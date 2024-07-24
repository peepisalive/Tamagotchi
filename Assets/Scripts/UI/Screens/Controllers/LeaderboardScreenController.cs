using UnityEngine;

namespace UI.Controller.Screen
{
    public sealed class LeaderboardScreenController : ScreenController
    {
        [Header("Controller")]
        [SerializeField] private ImageButtonController _updateButton;

        public override void Setup()
        {
            base.Setup();
        }
    }
}