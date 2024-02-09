using Systems.Activities;
using System.Creation;
using Leopotam.Ecs;
using Components;
using Systems;

namespace Starter
{
    public sealed class GameProcessing : EcsProcessing
    {
        public override void InitSystems(EcsWorld world)
        {
            Systems = new EcsSystems(world);

            Systems
                .Add(new PopupSystem())
                .Add(new PetCreationSystem())
                .Add(new ParametersSystem())
                .Add(new BankAccountSystem())
                .Add(HealthActivities(world))
                .OneFrame<ChangeParameterEvent>()
                .OneFrame<ChangeBankAccountValueEvent>();

            Systems.Init();
        }

        private EcsSystems HealthActivities(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new TakeToVetActivitySystem())
                .Add(new SpaTreatmentsActivitySystem())
                .Add(new TrainingActivitySystem());
        }
    }
}