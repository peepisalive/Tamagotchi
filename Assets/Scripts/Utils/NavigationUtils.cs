using Components.Modules.Navigation;
using Modules.Navigation;
using Leopotam.Ecs;

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
    }
}