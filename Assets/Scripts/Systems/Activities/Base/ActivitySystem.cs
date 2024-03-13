using System.Collections.Generic;
using Modules.Navigation;
using Settings.Activity;
using Leopotam.Ecs;
using UI.Settings;
using System.Linq;
using Components;
using Settings;
using System;

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

                StartActivity(comp.IsEnable);

                ActivityFilter.GetEntity(i).Destroy();
            }
        }

        protected List<DropdownSettings> GetDropdownSettings<E>() where E : Enum
        {
            var types = Enum.GetValues(typeof(E)).Cast<E>();
            var dropdownSettings = new List<DropdownSettings>
            {
                new DropdownSettings
                {
                    Title = "test", // to do: edit this
                    DropdownContent = new List<DropdownContent>()
                }
            };

            foreach (var type in types)
            {
                dropdownSettings.First().DropdownContent.Add(new DropdownContent<E>
                {
                    Title = Settings.Localization.GetValueTypeContent(type),
                    Value = type
                });
            }

            return dropdownSettings;
        }

        protected abstract void StartActivity(bool isEnable);
    }
}