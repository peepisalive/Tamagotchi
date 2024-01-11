using Leopotam.Ecs;
using Systems;
using System;

namespace Starter
{
    public sealed class GameProcessing : EcsProcessing
    {
        public override void InitSystems(EcsWorld world)
        {
            Systems = new EcsSystems(world);

            Systems
                .Add(new PetCreationSystem())
                .Add(new ParametersSystem());

            Systems.Init();
        }
    }
}