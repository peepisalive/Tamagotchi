using Leopotam.Ecs;
using Components;
using Core;

namespace System
{
    public sealed class PetCreationSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init()
        {
            _world.NewEntity().Replace(new PetComponent
            {
                Pet = new Pet(PetType.Frog, null, "")
            });
        }
    }
}