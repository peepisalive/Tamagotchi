using System.Collections.Generic;
using Settings.Activities;
using Modules.Navigation;
using Leopotam.Ecs;
using UI.Settings;
using Components;
using UI.Popups;

namespace Systems.Activities
{
    public sealed class SpaTreatmentsActivitySystem : ActivitySystem<SpaTreatmentsActivitySettings>
    {
        protected override NavigationElementType Type => NavigationElementType.SpaTreatmentsActivity;

        private EcsFilter<BankAccountComponent> _bankAccountFilter;

        protected override void StartActivity(bool isEnable)
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
                                World.NewEntity().Replace(new HidePopup());
                            }
                        },
                        new TextButtonSettings
                        {
                            Title = "Spa treatments", // to do: use localization system
                            Action = () =>
                            {
                                World.NewEntity().Replace(new ChangeBankAccountValueEvent
                                {
                                    Value = Settings.Price
                                });
                            }
                        }
                    }
                })
            });
        }
    }
}