using System.Collections.Generic;
using Modules.Navigation;
using Leopotam.Ecs;
using UI.Settings;
using Components;
using UI.Popups;
using Modules;
using Events;

namespace Systems.Activities
{
    public sealed class TakeToVetActivitySystem : ActivitySystem
    {
        protected override NavigationElementType Type => NavigationElementType.TakeToVetActivity;

        protected override void StartInteraction(bool isEnable)
        {
            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Title = "Close", // to do: use localization system
                            Action = () =>
                            {
                                EventSystem.Send<HidePopupEvent>();
                            }
                        },
                        new TextButtonSettings
                        {
                            Title = "Take to vet", // to do: use localization system
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