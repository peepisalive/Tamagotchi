using Application = Tamagotchi.Application;
using Components.Modules.Navigation;
using Leopotam.Ecs;
using Save.State;
using Modules;

namespace Systems.Modules.Navigation
{
    public sealed class NavigationTrackSystem : IEcsInitSystem, IEcsRunSystem, IStateLoadable
    {
        private GlobalStateHolder GlobalStateHolder
        {
            get
            {
                if (_stateHolder == null)
                    LoadState();

                return _stateHolder;
            }
        }
        private GlobalStateHolder _stateHolder;
        private EcsFilter<NavigationPointClickEvent> _navigationPointClickFilter;

        public void Init()
        {
            LoadState();
        }

        public void Run()
        {
            if (_navigationPointClickFilter.IsEmpty())
                return;

            foreach (var i in _navigationPointClickFilter)
            {
                GlobalStateHolder.State.NavigationTracks.Add(_navigationPointClickFilter.Get1(i).NavigationPoint.Type);
            }
        }

        public void LoadState()
        {
            _stateHolder = Application.SaveDataManager.GetStateHolder<GlobalStateHolder>();
        }
    }
}