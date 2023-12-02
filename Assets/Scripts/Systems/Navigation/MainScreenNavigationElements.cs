using Components.Modules.Navigation;
using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using Utils;

namespace Systems.Navigation
{
    public sealed class MainScreenNavigationElements : IEcsInitSystem, INavigationElement
    {
        public HashSet<NavigationElementType> Types => _types;

        private EcsFilter<BlockComponent> _blockFilter;
        private HashSet<NavigationElementType> _types = new HashSet<NavigationElementType>
        {
            NavigationElementType.MainScreen
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
            return IsEnable(elementType);
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