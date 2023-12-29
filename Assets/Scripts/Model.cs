using Components.Modules.Navigation;
using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using Components;
using Utils;
using Core;

namespace Tamagotchi
{
    public sealed class Model : IEcsSystem
    {
        private EcsFilter<BlockComponent, Active> _activeBlockFilter;
        private EcsFilter<BlockComponent> _blockFilter;
        private EcsFilter<PetComponent> _petFilter;

        #region Navigation
        public IEnumerable<NavigationPoint> GetChildPointsOfType(NavigationBlockType blockType, NavigationElementType type)
        {
            return _blockFilter.GetChildPointsOfType(blockType, type);
        }

        public NavigationBlockType? GetCurrentBlockType()
        {
            return _activeBlockFilter.GetCurrentBlockType();
        }

        public NavigationBlock GetCurrentNavigationBlock()
        {
            return _activeBlockFilter.GetCurrentNavigationBlock();
        }

        public NavigationPoint GetCurrentNavigationPoint()
        {
            return _activeBlockFilter.GetCurrentNavigationPoint();
        }
        #endregion

        public Pet GetCurrentPet()
        {
            foreach (var i in _petFilter)
            {
                return _petFilter.Get1(i).Pet;
            }

            return null;
        }
    }
}