using Application = Tamagotchi.Application;
using Components.Modules.Navigation;
using Systems.Modules.Navigation;
using Systems.Navigation;
using Leopotam.Ecs;
using Modules;

namespace Starter
{
    public sealed class NavigationStarter : Starter
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
                .Add(new HealthActivitiesNavigationElements())
                .Add(new SatietyActivitiesNavigationElements())
                .Add(new HappinessActivitiesNavigationElements())
                .Add(Application.Model)
                .Inject(new ScreenManager());

            Systems.Init();
        }
    }
}
