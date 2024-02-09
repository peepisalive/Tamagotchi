using Settings.Activities;
using Modules.Navigation;
using Leopotam.Ecs;
using Components;
using Settings;

namespace Systems.Activities
{
    public abstract class ActivitySystem<T> : IEcsInitSystem, IEcsRunSystem where T : ActivitySettings
    {
        protected abstract NavigationElementType Type { get; }

        protected EcsWorld World;
        protected EcsFilter<ActivityComponent> ActivityFilter;
        
        protected T Settings;

        public void Init()
        {
            Settings = SettingsProvider.Get<ActivitiesSettings>().Get<T>(Type);
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