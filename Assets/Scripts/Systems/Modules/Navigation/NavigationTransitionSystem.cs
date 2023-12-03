using Components.Modules.Navigation;
using Modules.Navigation;
using Leopotam.Ecs;
using Modules;

namespace Systems.Modules.Navigation
{
    public sealed class NavigationTransitionSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsWorld _world;

        private EcsFilter<BlockComponent> _blockFilter;
        private EcsFilter<BlockComponent, Active> _activeBlockFilter;

        private EcsFilter<NavigationActivateBlockEvent> _activateBlockFilter;
        private EcsFilter<NavigationPointClickEvent> _pointClickFilter;

        public void Init()
        {
            EventSystem.Subscribe<Events.NavigationPointClickEvent>(SendNavigationPointClickEvent);
        }

        public void Run()
        {
            if (!_activateBlockFilter.IsEmpty())
            {
                foreach (var i in _activateBlockFilter)
                {
                    var blockType = _activateBlockFilter.Get1(i).BlockType;

                    if (!blockType.HasValue)
                        continue;

                    ActivateNavigationBlock(blockType.Value);
                }
            }

            if (!_pointClickFilter.IsEmpty())
            {
                foreach (var i in _pointClickFilter)
                {
                    var nextNavigationPoint = _pointClickFilter.Get1(i).NavigationPoint;

                    if (nextNavigationPoint == null)
                        continue;

                    _world.NewEntity().Replace(new NavigationElementInteractionEvent
                    {
                        Type = nextNavigationPoint.Type,
                        Element = nextNavigationPoint.Element,
                        IsTransition = GoToNextPoint(nextNavigationPoint)
                    });
                }
            }
        }

        public void Destroy()
        {
            EventSystem.Unsubscribe<Events.NavigationPointClickEvent>(SendNavigationPointClickEvent);
        }

        private void ActivateNavigationBlock(NavigationBlockType type)
        {
            NavigationPoint previousPoint = null;

            foreach (var i in _activeBlockFilter)
            {
                if (_activeBlockFilter.Get2(i).Order != _activeBlockFilter.GetEntitiesCount() - 1)
                    continue;

                previousPoint = _activeBlockFilter.Get1(i).NavigationBlock.CurrentPoint;
            }

            foreach (var i in _blockFilter)
            {
                var block = _blockFilter.Get1(i).NavigationBlock;

                if (block.Type != type)
                    continue;

                var entity = _blockFilter.GetEntity(i);

                if (entity.Has<Active>())
                    continue;

                entity.Replace(new Active
                {
                    Order = _activeBlockFilter.GetEntitiesCount()
                });
                block.GoToRootPoint();

                _world.NewEntity().Replace(new NavigationPointChangedEvent
                {
                    CurrentPoint = block.CurrentPoint,
                    PreviousPoint = previousPoint,
                    TransitionType = TransitionType.In
                });

                break;
            }
        }

        private bool GoToNextPoint(NavigationPoint point)
        {
            var transition = false;

            foreach (var i in _activeBlockFilter)
            {
                if (_activeBlockFilter.Get2(i).Order != _activeBlockFilter.GetEntitiesCount() - 1)
                    continue;

                var block = _activeBlockFilter.Get1(i).NavigationBlock;
                var previousPoint = block.CurrentPoint;

                if (block.HandlePointClick(point))
                {
                    _world.NewEntity().Replace(new NavigationPointChangedEvent
                    {
                        PreviousPoint = previousPoint,
                        CurrentPoint = block.CurrentPoint,
                        TransitionType = TransitionType.In
                    });
                }
            }

            return transition;
        }

        private void SendNavigationPointClickEvent(Events.NavigationPointClickEvent e)
        {
            _world.NewEntity().Replace(new NavigationPointClickEvent
            {
                NavigationPoint = e.NavigationPoint
            });
        }
    }
}