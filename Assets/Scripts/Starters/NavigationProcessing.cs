using Application = Tamagotchi.Application;
using Components.Modules.Navigation;
using Systems.Modules.Navigation;
using Systems.Navigation;
using Leopotam.Ecs;
using Modules;

namespace Starter
{
    public sealed class NavigationProcessing : EcsProcessing
    {
        public override void InitSystems(EcsWorld world)
        {
            Systems = new EcsSystems(world);

            Systems
                .Add(new NavigationInitSystem())
                .Add(new NavigationTransitionSystem())
                .Add(new NavigationTrackSystem())
                .OneFrame<NavigationPointClickEvent>()
                .OneFrame<NavigationPointBackEvent>()
                .OneFrame<NavigationPointHomeEvent>()
                .OneFrame<NavigationActivateBlockEvent>()
                .Add(new NavigationScreenSystem())
                .OneFrame<NavigationElementInteractionEvent>()
                .OneFrame<NavigationPointChangedEvent>()
                .Add(ScreenElements(world))
                .Add(MenuScreenElements(world))
                .Add(ActivitiesScreenElements(world))
                .Add(PetActionsScreenElements(world))
                .Add(Application.Model)
                .Inject(new ScreenManager());

            Systems.Init();
        }

        private EcsSystems ScreenElements(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new MainScreenNavigationElements())
                .Add(new MenuScreenNavigationElements())
                .Add(new ActivitiesScreenNavigationElements())
                .Add(new PetActionsScreenNavigationElements())
                .Add(new JobScreenNavigationElements())
                .Add(new LeaderboardScreenNavigationElements());
        }

        private EcsSystems MenuScreenElements(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new SoundProviderNavigationElement())
                .Add(new HapticProviderNavigationElement())
                .Add(new LocalizationProviderNavigationElement())
                .Add(new ContactMeNavigationElement());
        }

        private EcsSystems PetActionsScreenElements(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new AccessoriesNavigationElement());
        }

        private EcsSystems ActivitiesScreenElements(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(HappinessActivitiesElements())
                .Add(SatietyActivitiesElements())
                .Add(HygieneActivitiesElements())
                .Add(HealthActivitiesElements());


            EcsSystems HappinessActivitiesElements()
            {
                return new EcsSystems(world)
                    .Add(new HappinessActivitiesNavigationElements())
                    .Add(new WalkNavigationElement())
                    .Add(new PlayNavigationElement());
            }

            EcsSystems SatietyActivitiesElements()
            {
                return new EcsSystems(world)
                    .Add(new SatietyActivitiesNavigationElements())
                    .Add(new FeedNavigationElement())
                    .Add(new DrinkNavigationElement())
                    .Add(new CookNavigationElement());
            }

            EcsSystems HygieneActivitiesElements()
            {
                return new EcsSystems(world)
                    .Add(new HygieneActivitiesNavigationElements())
                    .Add(new WashNavigationElement())
                    .Add(new CleanTheRoomNavigationElement())
                    .Add(new VentilateTheRoomNavigationElement());
            }

            EcsSystems HealthActivitiesElements()
            {
                return new EcsSystems(world)
                    .Add(new HealthActivitiesNavigationElements())
                    .Add(new TakeToVetNavigationElement())
                    .Add(new SpaTreatmentsNavigationElement())
                    .Add(TrainingActivities())
                    .Add(new VisitCosmetologistNavigationElement());


                EcsSystems TrainingActivities()
                {
                    return new EcsSystems(world)
                        .Add(new TrainingNavigationElement())
                        .Add(new YogaNavigationElement())
                        .Add(new StretchingNavigationElement())
                        .Add(new ExerciseNavigationElement());
                }
            }
        }
    }
}
