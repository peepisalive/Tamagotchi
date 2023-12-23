using Components.Modules.Navigation;
using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using Utils;

namespace Tamagotchi
{
    public sealed class Model : IEcsSystem
    {
        private EcsFilter<BlockComponent, Active> _activeBlockFilter;
        private EcsFilter<BlockComponent> _blockFilter;

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
    }
}