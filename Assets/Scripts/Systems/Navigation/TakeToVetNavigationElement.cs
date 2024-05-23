using Components.Modules.Navigation;
using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using Components;
using Utils;

namespace Systems.Navigation
{
    public sealed class TakeToVetNavigationElement : INavigationElement, IEcsInitSystem
    {
        public HashSet<NavigationElementType> Types => new HashSet<NavigationElementType>
        {
            NavigationElementType.TakeToVetActivity
        };

        private EcsWorld _world;
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
            _world.NewEntity().Replace(new ActivityComponent
            {
                Type = elementType,
                IsEnable = IsEnable(elementType)
            });

            return false;
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