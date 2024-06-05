using Application = Tamagotchi.Application;
using System.Collections.Generic;
using UnityEngine;
using UI.View;

namespace UI.Controller.Screen
{
    [RequireComponent(typeof(MainScreenView))]
    public sealed class MainScreenController : ScreenController
    {
        [Header("Controller")]
        [SerializeField] private MainScreenView _view;
        [SerializeField] private List<ParameterBarController> _parameterBarControllers;

        public override void Setup()
        {
            base.Setup();

            SetPetName();
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

        private void SetPetName()
        {
            _view.SetPetName(Application.Model.GetCurrentPet().Name);
        }
    }
}