using UnityEngine;
using UI.View;

namespace UI.Controller.Screen
{
    [RequireComponent(typeof(NewPetScreenView))]
    public sealed class NewPetScreenController : ScreenController
    {
        [Header("View")]
        [SerializeField] private NewPetScreenView _view;
    }
}