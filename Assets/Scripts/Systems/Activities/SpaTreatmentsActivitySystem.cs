using System.Collections.Generic;
using Modules.Navigation;
using Settings.Activity;
using Leopotam.Ecs;
using UI.Settings;
using Components;
using UI.Popups;
using Utils;

namespace Systems.Activities
{
    public sealed class SpaTreatmentsActivitySystem : ActivitySystem<PaidActivitySettings>
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
                    Content = "content",
                    DropdownSettings = GetDropdownSettings<SpaTreatmentsType>(),
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
                                if (!_bankAccountFilter.IsMoneyAvailable(Settings.Price))
                                {
                                    PopupUtils.ShowNotEnoughMoneyPopup();
                                    return;
                                }

                                World.NewEntity().Replace(new ChangeBankAccountValueEvent
                                {
                                    Value = Settings.Price
                                });
                                World.NewEntity().Replace(new HidePopup());
                            }
                        }
                    }
                })
            });
        }


        public enum SpaTreatmentsType
        {
            Massage = 0,
            Wrapping = 1,
            FaceMask = 2,
        }
    }
}