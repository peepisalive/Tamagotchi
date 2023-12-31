using Leopotam.Ecs;
using Components;
using System;

namespace Starter
{
    public sealed class ProcessingEcsStarter : Starter
    {
        public override void InitSystems(EcsWorld world)
        {
            Systems = new EcsSystems(world);

            Systems
                .Add(new SaveDataSystem())
                .OneFrame<SaveDataEvent>()
                .Add(new PetCreationSystem());

            Systems.Init();
        }
    }
}