using Settings.Activities;
using Modules.Navigation;
using Leopotam.Ecs;
using Components;
using Settings;

namespace Systems.Activities
{
    public abstract class ActivitySystem<T> : IEcsInitSystem, IEcsRunSystem where T : ActivitySettings
    {
        protected EcsWorld World;
        protected EcsFilter<ActivityComponent> ActivityFilter;
        
        protected T Settings;

        private NavigationElementType Type;

        public void Init()
        {
            Settings = SettingsProvider.Get<ActivitiesSettings>().Get<T>();
            Type = Settings.Type;
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

                StartActivity(comp.IsEnable);

                ActivityFilter.GetEntity(i).Destroy();
            }
        }

        protected abstract void StartActivity(bool isEnable);
    }
}