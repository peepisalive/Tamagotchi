using Modules.Navigation;
using Leopotam.Ecs;
using Components;

namespace Systems.Activities
{
    public abstract class ActivitySystem : IEcsRunSystem
    {
        protected abstract NavigationElementType Type { get; }

        protected EcsWorld World;
        protected EcsFilter<ActivityComponent> _activityFilter;

        public void Run()
        {
            if (_activityFilter.IsEmpty())
                return;

            foreach (var i in _activityFilter)
            {
                var comp = _activityFilter.Get1(i);

                if (comp.Type != Type)
                    continue;

                StartInteraction(comp.Type, comp.IsEnable);

                _activityFilter.GetEntity(i).Destroy();
            }
        }

        protected abstract void StartInteraction(NavigationElementType type, bool isEnable);
    }
}