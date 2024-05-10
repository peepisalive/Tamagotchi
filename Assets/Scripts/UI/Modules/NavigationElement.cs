using Settings.Modules.Navigations;
using Modules.Navigation;
using UnityEngine;
using Extensions;
using Settings;
using Modules;
using Events;

namespace UI.Modules.Navigation
{
    public sealed class NavigationElement : MonoBehaviour
    {
        public NavigationPoint NavigationPoint { get; private set; }
#if UNITY_EDITOR
        [ReadOnly]
        [SerializeField] private NavigationElementType _type;
#endif

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
                }
            }
        }

        public void OnClick()
        {
            EventSystem.Send(new NavigationPointClickEvent
            {
                NavigationPoint = NavigationPoint
            });
        }
    }
}