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

        private EcsFilter<NavigationPointBackEvent> _backClickFilter;
        private EcsFilter<NavigationPointHomeEvent> _homeClickEvent;

        public void Init()
        {
            EventSystem.Subscribe<Events.NavigationPointClickEvent>(SendNavigationPointClickEvent);
            EventSystem.Subscribe<Events.NavigationPointBackEvent>(SendNavigationPointBackEvent);
            EventSystem.Subscribe<Events.NavigationPointHomeEvent>(SendNavigationPointHomeEvent);
        }

        public void Destroy()
        {
            EventSystem.Unsubscribe<Events.NavigationPointClickEvent>(SendNavigationPointClickEvent);
            EventSystem.Unsubscribe<Events.NavigationPointBackEvent>(SendNavigationPointBackEvent);
            EventSystem.Unsubscribe<Events.NavigationPointHomeEvent>(SendNavigationPointHomeEvent);
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

            if (!_backClickFilter.IsEmpty())
            {
                foreach (var i in _backClickFilter)
                {
                    GoToBack();
                    return;
                }
            }

            if (!_homeClickEvent.IsEmpty())
            {
                foreach (var i in _homeClickEvent)
                {
                    GoToHome();
                    return;
                }
            }
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

        private void GoToBack()
        {
            NavigationPoint previousPoint = null;
            NavigationPoint currentPoint = null;
            var blockChanged = false;

            foreach (var i in _activeBlockFilter)
            {
                if (_activeBlockFilter.Get2(i).Order != _blockFilter.GetEntitiesCount() - 1)
                    continue;

                var block = _activeBlockFilter.Get1(i).NavigationBlock;

                if (_activeBlockFilter.GetEntitiesCount() == 1 && block.CurrentPoint.Type == block.RootElementType)
                    continue;

                previousPoint = block.CurrentPoint;
                block.GoToPreviousPoint();

                if (block.NavigationChain.Count == 0)
                {
                    blockChanged = true;
                    _activateBlockFilter.GetEntity(i).Del<Active>();
                }
                else
                {
                    currentPoint = block.CurrentPoint;
                }
            }

            if (previousPoint == null)
                return;

            if (blockChanged)
            {
                foreach (var i in _activeBlockFilter)
                {
                    if (_activeBlockFilter.Get2(i).Order != _activeBlockFilter.GetEntitiesCount() - 1)
                        continue;

                    currentPoint = _activeBlockFilter.Get1(i).NavigationBlock.CurrentPoint;
                }
            }

            _world.NewEntity().Replace(new NavigationPointChangedEvent
            {
                CurrentPoint = currentPoint,
                PreviousPoint = previousPoint,
                TransitionType = TransitionType.Out
            });
        }

        private void GoToHome()
        {
            NavigationPoint previousPoint = null;

            foreach (var i in _activeBlockFilter)
            {
                if (_activeBlockFilter.Get2(i).Order != _activeBlockFilter.GetEntitiesCount() - 1)
                    continue;

                var block = _activeBlockFilter.Get1(i).NavigationBlock;

                if (_activeBlockFilter.GetEntitiesCount() == 1 && block.CurrentPoint.Type == block.RootElementType)
                    continue;

                previousPoint = block.CurrentPoint;
            }

            if (previousPoint == null)
                return;

            foreach (var i in _activeBlockFilter)
            {
                _activeBlockFilter.Get1(i).NavigationBlock.ClearNavigationChain();
                _activeBlockFilter.GetEntity(i).Del<Active>();
            }

            foreach (var i in _blockFilter)
            {
                var block = _blockFilter.Get1(i).NavigationBlock;

                if (block.Type != NavigationBlockType.Main)
                    continue;

                block.GoToRootPoint();

                _blockFilter.GetEntity(i).Replace(new Active
                {
                    Order = 0
                });
                _world.NewEntity().Replace(new NavigationPointChangedEvent
                {
                    PreviousPoint = previousPoint,
                    CurrentPoint = block.CurrentPoint,
                    TransitionType = TransitionType.Out
                });
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

        private void SendNavigationPointBackEvent(Events.NavigationPointBackEvent e)
        {
            _world.NewEntity().Replace(new NavigationPointBackEvent());
        }

        private void SendNavigationPointHomeEvent(Events.NavigationPointHomeEvent e)
        {
            _world.NewEntity().Replace(new NavigationPointHomeEvent());
        }
    }
}