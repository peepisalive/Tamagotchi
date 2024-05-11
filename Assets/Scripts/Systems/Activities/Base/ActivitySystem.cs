using Settings.Modules.Navigations;
using System.Collections.Generic;
using Modules.Navigation;
using Settings.Activity;
using Leopotam.Ecs;
using UI.Settings;
using System.Linq;
using UnityEngine;
using Components;
using UI.Popups;
using Settings;
using System;

namespace Systems.Activities
{
    public abstract class ActivitySystem<T> : IEcsInitSystem, IEcsRunSystem where T : ActivitySettings
    {
        protected abstract NavigationElementType Type { get; }

        protected EcsWorld World;
        protected EcsFilter<PetComponent> PetFilter;
        protected EcsFilter<ActivityComponent> ActivityFilter;

        protected T Settings;
        protected Sprite Icon;

        public void Init()
        {
            Settings = SettingsProvider.Get<ActivitiesSettings>().Get<T>(Type);
            Icon = SettingsProvider.Get<NavigationIconSettings>().GetIcon(Type);
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
                    Title = Settings.Localization.TypeName,
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

        protected virtual List<InfoParameterSettings> GetInfoParameterSettings()
        {
            var infoParametersSettings = new List<InfoParameterSettings>();

            foreach (var i in PetFilter)
            {
                var pet = PetFilter.Get1(i).Pet;

                foreach (var parameterChange in Settings.ParametersChanges)
                {
                    pet.Parameters.Get(parameterChange.Type).Add(parameterChange.Range.GetRandom());
                    infoParametersSettings.Add(new InfoParameterSettings
                    {
                        Type = parameterChange.Type,
                        Value = pet.Parameters.Get(parameterChange.Type).Value
                    });
                }
            }

            return infoParametersSettings;
        }

        protected virtual void EndActivity(bool useIcon, bool usePetIcon)
        {
            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<ResultPopup>(new ResultPopup()
                {
                    Title = Settings.Localization.Title,
                    Icon = Icon,
                    Content = Settings.Localization.ResultContent,
                    InfoParameterSettings = GetInfoParameterSettings(),
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Title = Settings.Localization.ResultButton,
                            Action = () =>
                            {
                                World.NewEntity().Replace(new HidePopup());
                            }
                        }
                    },
                    UseIcon = useIcon,
                    UsePetIcon = usePetIcon
                })
            });
        }

        protected abstract void StartActivity(bool isEnable);
    }
}