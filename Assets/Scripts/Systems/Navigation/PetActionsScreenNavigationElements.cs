using Components.Modules.Navigation;
using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using Components;
using Utils;

namespace Systems.Navigation
{
    public sealed class PetActionsScreenNavigationElements : IEcsInitSystem, INavigationElement
    {
        public HashSet<NavigationElementType> Types => new HashSet<NavigationElementType>
        {
            NavigationElementType.PetActionsScreen,
        };

        private EcsFilter<JobComponent> _jobFilter;
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
            var result = true;

            foreach (var i in _jobFilter)
            {
                result = _jobFilter.Get1(i).CurrentFullTimeJob == null;
            }

            return result;
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