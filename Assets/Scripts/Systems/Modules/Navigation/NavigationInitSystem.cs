using Components.Modules.Navigation;
using Settings.Modules.Navigations;
using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using Settings;

namespace Systems.Modules.Navigation
{
    public sealed class NavigationInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init()
        {
            SetupNavigationBlocks();
        }

        private void SetupNavigationBlocks()
        {
            var settings = SettingsProvider.Get<NavigationSettings>();
            var blockTypes = new List<NavigationBlockType>
            {
                NavigationBlockType.Main
            };

            blockTypes.ForEach(blockType =>
            {
                if (!settings.TryGetSet(blockType, out var blockSettings))
                    return;

                _world.NewEntity().Replace(new BlockComponent
                {
                    NavigationBlock = new NavigationBlock
                    (
                        blockSettings.BlockType,
                        blockSettings.RootElementType,
                        blockSettings.ElementsSet
                    )
                });
            });
        }
    }
}