using Application = Tamagotchi.Application;
using Leopotam.Ecs;
using UnityEngine;
using Components;
using System;

namespace Starter
{
    public sealed class SaveProcessingStarter : Starter
    {
        public override void InitSystems(EcsWorld world)
        {
            Systems = new EcsSystems(world);

            Systems
                .Add(new SaveDataSystem())
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