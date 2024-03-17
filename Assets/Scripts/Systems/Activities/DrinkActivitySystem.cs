using System.Collections.Generic;
using Modules.Navigation;
using Settings.Activity;
using Leopotam.Ecs;
using UI.Settings;
using Components;
using UI.Popups;

namespace Systems.Activities
{
    public sealed class DrinkActivitySystem : ActivitySystem<FreeActivitySettings>
    {
        protected override NavigationElementType Type => NavigationElementType.DrinkActivity;

        protected override void StartActivity(bool isEnable)
        {
            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = Settings.Localization.Title,
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

                            }
                        }
                    }
                })
            });
        }
    }
}