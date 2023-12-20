using Application = Tamagotchi.Application;
using Components.Modules.Navigation;
using Systems.Modules.Navigation;
using Systems.Navigation;
using Leopotam.Ecs;
using UnityEngine;
using Modules;

public sealed class NavigationStarter : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _navigationSystems;

    private void Update()
    {
        _navigationSystems.Run();
    }

    private void Awake()
    {
        _world = new EcsWorld();
        _navigationSystems = new EcsSystems(_world);

        _navigationSystems
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
            .Add(Application.Model)
            .Inject(new ScreenManager());

        _navigationSystems.Init();
    }
    
    private void OnDestroy()
    {
        _navigationSystems?.Destroy();
        _navigationSystems = null;
    }
}
