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
                .OneFrame<NavigationPointClickEvent>()
                .OneFrame<NavigationPointBackEvent>()
                .OneFrame<NavigationPointHomeEvent>()
                .OneFrame<NavigationActivateBlockEvent>()
                .Add(new NavigationScreenSystem())
                .OneFrame<NavigationElementInteractionEvent>()
                .OneFrame<NavigationPointChangedEvent>()
                .Add(new MainScreenNavigationElements())
                .Add(new MenuScreenNavigationElements())
                .Add(new ActivitiesScreenNavigationElements())
                .Add(HappinessActivitiesElements(world))
                .Add(SatietyActivitiesElements(world))
                .Add(HygieneActivitiesElements(world))
                .Add(HealthActivitiesElements(world))
                .Add(Application.Model)
                .Inject(new ScreenManager());

            Systems.Init();
        }

        private EcsSystems HappinessActivitiesElements(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new HappinessActivitiesNavigationElements())
                .Add(new WalkNavigationElement())
                .Add(new PlayNavigationElement());
        }

        private EcsSystems SatietyActivitiesElements(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new SatietyActivitiesNavigationElements())
                .Add(new FeedNavigationElement())
                .Add(new DrinkNavigationElement());
        }

        private EcsSystems HygieneActivitiesElements(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new HygieneActivitiesNavigationElements())
                .Add(new WashNavigationElement());
        }

        private EcsSystems HealthActivitiesElements(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new HealthActivitiesNavigationElements())
                .Add(new TakeToVetNavigationElement())
                .Add(new SpaTreatmentsNavigationElement())
                .Add(new TrainingNavigationElement())
                .Add(new YogaNavigationElement());
        }
    }
}
