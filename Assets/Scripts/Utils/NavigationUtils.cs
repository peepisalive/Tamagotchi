using Components.Modules.Navigation;
using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using System.Linq;

namespace Utils
{
    public static class NavigationUtils
    {
        public static void RegisterElement(this EcsFilter<BlockComponent> filter, NavigationBlockType blockType, INavigationElement element)
        {
            foreach (var i in filter)
            {
                var block = filter.Get1(i).NavigationBlock;

                if (block.Type != blockType)
                    continue;

                block.RegisterElement(element);
            }
        }

        public static IEnumerable<NavigationPoint> GetChildPointsOfType(this EcsFilter<BlockComponent> filter, NavigationBlockType blockType, NavigationElementType type)
        {
            foreach (var i in filter)
            {
                var block = filter.Get1(i).NavigationBlock;

                if (block.Type != blockType)
                    continue;

                return block.GetChildPointsOfType(type);
            }

            return Enumerable.Empty<NavigationPoint>();
        }

        public static NavigationBlockType? GetCurrentBlockType(this EcsFilter<BlockComponent, Active> filter)
        {
            foreach (var i in filter)
            {
                if (filter.Get2(i).Order != filter.GetEntitiesCount() - 1)
                    continue;

                return filter.Get1(i).NavigationBlock.Type;
            }

            return null;
        }

        public static NavigationButtonData GetNavigationButtonData(this EcsFilter<BlockComponent> filter, NavigationBlockType blockType, NavigationElementType type, INavigationElement element)
        {
            foreach (var i in filter)
            {
                var block = filter.Get1(i).NavigationBlock;

                if (block.Type != blockType)
                    continue;
                
                return block.GetNavigationButtonData(type, element);
            }

            return null;
        }
    }
}