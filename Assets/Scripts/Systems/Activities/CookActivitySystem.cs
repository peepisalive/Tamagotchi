using System.Collections.Generic;
using Modules.Navigation;
using Settings.Activity;
using Leopotam.Ecs;
using UI.Settings;
using Components;
using UI.Popups;

namespace Systems.Activities
{
    public sealed class CookActivitySystem : ActivitySystem<FreeActivitySettings>
    {
        protected override NavigationElementType Type => NavigationElementType.CookActivity;

        protected override void StartActivity(bool isEnable)
        {
            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = Settings.Localization.Title,
                    Icon = Icon,
                    Content = Settings.Localization.MainContent,
                    DropdownSettings = GetDropdownSettings<FoodType>(),
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Title = Settings.Localization.LeftButtonContent,
                            Action = () =>
                            {
                                World.NewEntity().Replace(new HidePopup());
                            }
                        },
                        new TextButtonSettings
                        {
                            Title = Settings.Localization.RightButtonContent,
                            Action = () =>
                            {
                                EndActivity(true, false);
                            }
                        }
                    },
                    UseIcon = true
                })
            });
        }


        public enum FoodType : sbyte
        {
            Fried = 0,
            Boiled = 1,
            Steamed = 2,
        }
    }
}