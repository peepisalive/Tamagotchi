using UI.Controller.Screen;
using UnityEngine;

namespace UI.Controller
{
    public sealed class NewPetScreenController : ScreenController
    {
        [Header("Controller")]
        [SerializeField] private PetChanger _petChanger;

        public override void Setup()
        {
            base.Setup();
            _petChanger.Setup();
        }
    }
}