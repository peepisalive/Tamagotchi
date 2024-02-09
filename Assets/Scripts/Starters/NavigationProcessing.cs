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
                .Add(HealthActivities(world))
                .Add(SatietyActivities(world))
                .Add(HappinessActivities(world))
                .Add(Application.Model)
                .Inject(new ScreenManager());

            Systems.Init();
        }

        private EcsSystems HealthActivities(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new HealthActivitiesNavigationElements())
                .Add(new TakeToVetNavigationElement())
                .Add(new SpaTreatmentsNavigationElement());
        }

        private EcsSystems SatietyActivities(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new SatietyActivitiesNavigationElements());
        }

        private EcsSystems HappinessActivities(EcsWorld world)
        {
            return new EcsSystems(world)
                .Add(new HappinessActivitiesNavigationElements());
        }
    }
}
