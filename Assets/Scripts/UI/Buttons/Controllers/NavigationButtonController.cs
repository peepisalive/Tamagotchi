using UnityEngine;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(NavigationButtonView))]
    public sealed class NavigationButtonController : ButtonController
    {
        [Header("Controller")]
        [SerializeField] private NavigationButtonView _view;
    }
}