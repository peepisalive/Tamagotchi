using Components.Modules.Navigation;
using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using Utils;

namespace Systems.Navigation
{
    public sealed class NewPetNavigationElement : INavigationElement, IEcsInitSystem
    {
        public HashSet<NavigationElementType> Types => new HashSet<NavigationElementType>
        {
            NavigationElementType.NewPet
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
            return true;
        }

        public NavigationButtonData GetButtonData(NavigationElementType elementType)
        {
            return _blockFilter.GetNavigationButtonData(NavigationBlockType.Main, elementType, this);
        }

        public NavigationScreenData GetScreenData(NavigationElementType elementType)
        {
            return _blockFilter.GetNavigationScreenData(NavigationBlockType.Main, elementType);
        }
    }
}