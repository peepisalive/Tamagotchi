using Systems.Activities;
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
                .Add(new JobSystem())
                .Add(new PetSystem())
                .Add(new ParametersSystem())
                .Add(new BankAccountSystem())
                .Add(HappinessActivitiesElements(world))
                .Add(SatietyActivitiesElements(world))
                .Add(HygieneActivities(world))
                .Add(HealthActivities(world))
                .OneFrame<ChangeParameterEvent>()
                .OneFrame<ChangeBankAccountValueEvent>()
                .Add(JobOneFrameEvents(world))
                .Add(AnimationOneFrameEvents(world));

            Systems.Init();
        }

        private EcsSystems HappinessActivitiesElements(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new WalkActivitySystem())
                .Add(new PlayActivitySystem());
        }

        private EcsSystems SatietyActivitiesElements(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new FeedActivitySystem())
                .Add(new DrinkActivitySystem())
                .Add(new CookActivitySystem());
        }

        private EcsSystems HygieneActivities(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new WashActivitySystem())
                .Add(new CleanTheRoomActivitySystem())
                .Add(new VentilateTheRoomActivitySystem());
        }

        private EcsSystems HealthActivities(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(TrainingActivities())
                .Add(new TakeToVetActivitySystem())
                .Add(new SpaTreatmentsActivitySystem())
                .Add(new VisitCosmetologistActivitySystem());

            
            EcsSystems TrainingActivities()
            {
                return new EcsSystems(world)
                    .Add(new YogaActivitySystem())
                    .Add(new StretchingActivitySystem())
                    .Add(new ExerciseActivitySystem());
            }
        }

        private EcsSystems JobOneFrameEvents(EcsWorld world)
        {
            return new EcsSystems(world)
                .OneFrame<GettingJobEvent>()
                .OneFrame<EndOfFullTimeJobEvent>()
                .OneFrame<EndOfRecoveryPartTimeEvent>();
        }

        private EcsSystems AnimationOneFrameEvents(EcsWorld world)
        {
            return new EcsSystems(world)
                .OneFrame<ChangePetAnimationEvent>()
                .OneFrame<ChangePetEyesAnimationEvent>();
        }
    }
}