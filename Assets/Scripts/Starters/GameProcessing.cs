using Leopotam.Ecs;
using System;

namespace Starter
{
    public sealed class GameProcessing : EcsProcessing
    {
        public override void InitSystems(EcsWorld world)
        {
            Systems = new EcsSystems(world);

            Systems
                .Add(new PetCreationSystem());

            Systems.Init();
        }
    }
}