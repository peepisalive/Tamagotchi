using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using Components.Modules.Navigation;
using Utils;

namespace Systems.Navigation
{
    public sealed class ActivitiesScreenNavigationElements : IEcsInitSystem, INavigationElement
    {
        private EcsFilter<BlockComponent> _blockFilter;

        public HashSet<NavigationElementType> Types => new HashSet<NavigationElementType>
        {
            NavigationElementType.ActivitiesScreen,
        };

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
            return true;
        }

        public NavigationButtonData GetButtonData(NavigationElementType elementType)
        {
            return null;
        }

        public NavigationScreenData GetScreenData(NavigationElementType elementType)
        {
            return null;
        }
    }
}