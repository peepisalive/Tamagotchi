using Application = Tamagotchi.Application;
using UI.Modules.Navigation;
using Modules.Navigation;
using UnityEngine;
using System.Linq;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(MainScreenNavButtonView), typeof(NavigationElement))]
    public sealed class MainScreenNavButtonController : MonoBehaviour
    {
        [SerializeField] private NavigationElementType _type;

        [Space][SerializeField] private NavigationElement _element;
        [SerializeField] private MainScreenNavButtonView _view;

        private void Setup()
        {
            var currentBlockType = Application.Model.GetCurrentBlockType();

            if (!currentBlockType.HasValue)
                return;

            var navigationPoint = Application.Model.GetChildPointsOfType(currentBlockType.Value, NavigationElementType.MainScreen)
                .FirstOrDefault(np => np.Type == _type);

            if (navigationPoint == null)
                return;

            _element.Setup(navigationPoint, currentBlockType.Value);
        }

        private void Start()
        {
            Setup();
        }
    }
}