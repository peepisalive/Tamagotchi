using Application = Tamagotchi.Application;
using Leopotam.Ecs;
using Components;
using Systems;

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

        private void OnApplicationPause(bool pause)
        {
            Systems?.Run();

            if (pause)
            {
                Application.Model.Send(new SaveDataEvent
                {
                    IsAsync = false
                });
            }
        }

#if UNITY_EDITOR
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
#endif
    }
}