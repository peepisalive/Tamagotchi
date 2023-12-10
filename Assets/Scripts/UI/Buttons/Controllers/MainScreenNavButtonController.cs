using Application = Tamagotchi.Application;
using UI.Modules.Navigation;
using Modules.Navigation;
using UnityEngine;
using System.Linq;
using UI.View;
using Modules;
using Events;

namespace UI.Controller
{
    [RequireComponent(typeof(MainScreenNavButtonView))]
    [RequireComponent(typeof(NavigationElement))]
    public sealed class MainScreenNavButtonController : MonoBehaviour, IUpdatable<UpdateCurrentScreen>
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
            UpdateState();
        }

        public void UpdateState(UpdateCurrentScreen data = null)
        {
            
        }

        private void Start()
        {
            Setup();
        }

        private void Awake()
        {
            EventSystem.Subscribe<UpdateCurrentScreen>(UpdateState);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<UpdateCurrentScreen>(UpdateState);
        }
    }
}