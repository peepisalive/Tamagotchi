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

    private void Start()
    {
        _world = new EcsWorld();
        _navigationSystems = new EcsSystems(_world);

        _navigationSystems
            .Add(new NavigationInitSystem())
            .Add(new NavigationTransitionSystem())
            .OneFrame<NavigationPointClickEvent>()
            .OneFrame<NavigationActivateBlockEvent>()
            .Add(new NavigationScreenSystem())
            .OneFrame<NavigationElementInteractionEvent>()
            .OneFrame<NavigationPointChangedEvent>()
            .Add(new MainScreenNavigationElements())
            .Inject(new ScreenManager());

        _navigationSystems.Init();
    }
    
    private void OnDestroy()
    {
        _navigationSystems?.Destroy();
        _navigationSystems = null;
    }
}
