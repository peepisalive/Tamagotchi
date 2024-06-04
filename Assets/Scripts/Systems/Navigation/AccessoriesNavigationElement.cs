using Application = Tamagotchi.Application;
using Components.Modules.Navigation;
using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using Components;
using Utils;

namespace Systems.Navigation
{
    public sealed class AccessoriesNavigationElement : INavigationElement, IEcsInitSystem
    {
        public HashSet<NavigationElementType> Types => new HashSet<NavigationElementType>
        {
            NavigationElementType.Accessories
        };

        private EcsFilter<BlockComponent> _blockFilter;
        private EcsFilter<PetComponent, DeadComponent> _deadPetFilter;

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
            return _deadPetFilter.IsEmpty();
        }

        public bool NotificationIsEnable(NavigationElementType elementType)
        {
            return !Application.HasTrack(elementType) & IsEnable(elementType);
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