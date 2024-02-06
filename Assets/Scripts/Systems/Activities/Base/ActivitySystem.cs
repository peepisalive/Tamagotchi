using Settings.Activities;
using Modules.Navigation;
using Leopotam.Ecs;
using Components;
using Settings;

namespace Systems.Activities
{
    public abstract class ActivitySystem : IEcsInitSystem, IEcsRunSystem
    {
        protected abstract NavigationElementType Type { get; }

        protected EcsWorld World;
        protected EcsFilter<ActivityComponent> ActivityFilter;
        
        protected ActivitySettings ActivitySettings;

        public void Init()
        {
            ActivitySettings = SettingsProvider.Get<ActivitiesSettings>().Get(Type);
        }

        public void Run()
        {
            if (ActivityFilter.IsEmpty())
                return;

            foreach (var i in ActivityFilter)
            {
                var comp = ActivityFilter.Get1(i);

                if (comp.Type != Type)
                    continue;

                StartInteraction(comp.IsEnable);

                ActivityFilter.GetEntity(i).Destroy();
            }
        }

        protected abstract void StartInteraction(bool isEnable);
    }
}