using Modules.Navigation;
using Leopotam.Ecs;
using Components;

namespace Systems.Activities
{
    public abstract class ActivitySystem : IEcsRunSystem
    {
        protected abstract NavigationElementType Type { get; }

        protected EcsWorld World;
        protected EcsFilter<ActivityComponent> ActivityFilter;

        public void Run()
        {
            if (ActivityFilter.IsEmpty())
                return;

            foreach (var i in ActivityFilter)
            {
                var comp = ActivityFilter.Get1(i);

                if (comp.Type != Type)
                    continue;

                StartInteraction(comp.Type, comp.IsEnable);

                ActivityFilter.GetEntity(i).Destroy();
            }
        }

        protected abstract void StartInteraction(NavigationElementType type, bool isEnable);
    }
}