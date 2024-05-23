using Application = Tamagotchi.Application;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Controller.Screen
{
    public sealed class MainScreenController : ScreenController
    {
        [Header("Controller")]
        [SerializeField] private List<ParameterBarController> _parameterBarControllers;

        public override void Setup()
        {
            base.Setup();

            SetupParameterBars();
        }

        private void SetupParameterBars()
        {
            var pet = Application.Model.GetCurrentPet();

            _parameterBarControllers.ForEach(controller =>
            {
                controller.Setup(pet.Parameters.Get(controller.Type));
            });
        }
    }
}