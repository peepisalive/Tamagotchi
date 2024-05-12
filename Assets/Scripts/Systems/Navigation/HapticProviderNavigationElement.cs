using Components.Modules.Navigation;
using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using Modules;
using Events;
using Utils;

namespace Systems.Navigation
{
    public sealed class HapticProviderNavigationElement : IEcsInitSystem, INavigationElement
    {
        public HashSet<NavigationElementType> Types => new HashSet<NavigationElementType>
        {
            NavigationElementType.HapticProvider
        };

        private EcsFilter<BlockComponent> _blockFilter;

        public void Init()
        {
            _blockFilter.RegisterElement(NavigationBlockType.Main, this);
        }

        public bool CanDisplay(NavigationElementType elementType)
        {
            return true;
        }

        public bool IsEnable(NavigationElementType elementType)
        {
            return true;
        }

        public bool NotificationIsEnable(NavigationElementType elementType)
        {
            return false;
        }

        public bool OnClick(NavigationElementType elementType)
        {
            HapticProvider.Instance.SwitchState();
            EventSystem.Send(new NavigationToggleUpdateStateEvent
            {
                Type = NavigationElementType.HapticProvider,
                State = HapticProvider.Instance.State
            });

            return false;
        }

        public NavigationButtonData GetButtonData(NavigationElementType elementType)
        {
            var buttonData = _blockFilter.GetNavigationButtonData(NavigationBlockType.Main, elementType, this);

            buttonData.IsToggleButton = true;
            buttonData.DefaultToggleState = HapticProvider.Instance.State;

            return buttonData;
        }

        public NavigationScreenData GetScreenData(NavigationElementType elementType)
        {
            return _blockFilter.GetNavigationScreenData(NavigationBlockType.Main, elementType);
        }
    }
}