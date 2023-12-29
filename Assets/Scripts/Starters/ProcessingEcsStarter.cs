using Leopotam.Ecs;
using Systems;

namespace Starter
{
    public sealed class ProcessingEcsStarter : Starter
    {
        public override void InitSystems(EcsWorld world)
        {
            Systems = new EcsSystems(world);

            Systems
                .Add(new PetCreationSystem());

            Systems.Init();
        }

        //private void OnDestroy()
        //{
        //    _systems?.Destroy();
        //    _systems = null;

        //    _world?.Destroy();
        //    _world = null;
        //}
    }
}