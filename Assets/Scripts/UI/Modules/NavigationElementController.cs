using Settings.Modules.Navigations;
using Modules.Navigation;
using UnityEngine.UI;
using UnityEngine;
using Extensions;
using Settings;
using Modules;
using Events;

namespace UI.Modules.Navigation
{
    [RequireComponent(typeof(NavigationElementView))]
    public sealed class NavigationElementController : MonoBehaviour
    {
        public NavigationPoint NavigationPoint { get; private set; }
#if UNITY_EDITOR
        [ReadOnly]
        [SerializeField] private NavigationElementType _type;
#endif
        [SerializeField] private NavigationElementView _view;
        [SerializeField] private Button _button;

        public void Setup(NavigationPoint navigationPoint, NavigationBlockType blockType)
        {
            var settings = SettingsProvider.Get<NavigationSettings>();

            if (settings.TryGetSet(blockType, out var navigationSet))
            {
                if (navigationSet.ElementsSet.TryGetElementSettings(navigationPoint.Type, out var elementSettings))
                {
                    NavigationPoint = navigationPoint;
#if UNITY_EDITOR
                    _type = navigationPoint.Type;
#endif
                    _button.interactable = NavigationPoint.Element.IsEnable(NavigationPoint.Type);
                    _view.SetNotifyIconState(NavigationPoint.Element.NotificationIsEnable(NavigationPoint.Type));
                }
            }
        }

        private void OnClick()
        {
            EventSystem.Send(new NavigationPointClickEvent
            {
                NavigationPoint = NavigationPoint
            });
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick?.RemoveListener(OnClick);
        }
    }
}