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
    [RequireComponent(typeof(MainScreenNavButtonView), typeof(NavigationElement))]
    public sealed class MainScreenNavButtonController : MonoBehaviour, IUpdatable<UpdateCurrentScreenEvent>
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

        public void UpdateState(UpdateCurrentScreenEvent data = null)
        {
            
        }

        private void Start()
        {
            Setup();
        }

        private void Awake()
        {
            EventSystem.Subscribe<UpdateCurrentScreenEvent>(UpdateState);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<UpdateCurrentScreenEvent>(UpdateState);
        }
    }
}