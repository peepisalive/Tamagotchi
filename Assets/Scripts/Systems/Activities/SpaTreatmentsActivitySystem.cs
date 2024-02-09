using System.Collections.Generic;
using Settings.Activities;
using Modules.Navigation;
using Leopotam.Ecs;
using UI.Settings;
using Components;
using UI.Popups;
using Modules;
using Events;

namespace Systems.Activities
{
    public sealed class SpaTreatmentsActivitySystem : ActivitySystem<TakeToVetActivitySettings>
    {
        protected override NavigationElementType Type => NavigationElementType.SpaTreatmentsActivity;

        protected override void StartInteraction(bool isEnable)
        {
            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = Type.ToString(), // to do: edit this
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
                            Title = "Spa treatments", // to do: use localization system
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