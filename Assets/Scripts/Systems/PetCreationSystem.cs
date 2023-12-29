using Leopotam.Ecs;
using Components;
using Core;

namespace Systems
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