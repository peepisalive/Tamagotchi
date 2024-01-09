using Application = Tamagotchi.Application;
using Leopotam.Ecs;
using Components;
using System;

namespace Starter
{
    public sealed class SaveProcessing : EcsProcessing
    {
        public override void InitSystems(EcsWorld world)
        {
            Systems = new EcsSystems(world);

            Systems
                .Add(new SaveDataSystem())
                .OneFrame<SaveDataEvent>()
                .Inject(Application.SaveDataManager)
                .Init();
        }

        private void OnApplicationFocus(bool focus)
        {
            Systems?.Run();

            if (!focus)
            {
                Application.Model.Send(new SaveDataEvent
                {
                    IsAsync = false
                });
            }
        }
    }
}