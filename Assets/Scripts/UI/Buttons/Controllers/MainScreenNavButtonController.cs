using Application = Tamagotchi.Application;
using UI.Modules.Navigation;
using Modules.Localization;
using Modules.Navigation;
using UnityEngine;
using System.Linq;
using UI.View;
using Modules;
using Events;

namespace UI.Controller
{
    [RequireComponent(typeof(MainScreenNavButtonView), typeof(NavigationElementController))]
    public sealed class MainScreenNavButtonController : MonoBehaviour, IUpdatable<UpdateCurrentScreenEvent>
    {
        [SerializeField] private NavigationElementType _type;

        [Space][SerializeField] private NavigationElementController _element;
        [SerializeField] private MainScreenNavButtonView _view;

        public void UpdateState(UpdateCurrentScreenEvent data = null)
        {
            var currentBlockType = Application.Model.GetCurrentBlockType();

            if (!currentBlockType.HasValue)
                return;

            var navigationPoint = Application.Model.GetChildPointsOfType(currentBlockType.Value, NavigationElementType.MainScreen)
                .FirstOrDefault(np => np.Type == _type);

            if (navigationPoint == null)
                return;

            _view.SetTitle(LocalizationProvider.GetNavigationText($"navigation_title_{navigationPoint.Type}"));
            _element.Setup(navigationPoint, currentBlockType.Value);
        }

        private void Setup()
        {
            UpdateState();
        }

        private void Start()
        {
            Setup();
            EventSystem.Subscribe<UpdateCurrentScreenEvent>(UpdateState);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<UpdateCurrentScreenEvent>(UpdateState);
        }
    }
}