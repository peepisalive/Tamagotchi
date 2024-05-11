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
        private SpaTreatmentsType _selectedSpaTreatmentsType;

        protected override void StartActivity(bool isEnable)
        {
            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = Settings.Localization.Title,
                    Icon = Icon,
                    Content = Settings.Localization.MainContent,
                    DropdownSettings = GetDropdownSettings<SpaTreatmentsType>(),
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
                            ActionWithInstance = (popup) =>
                            {
                                if (!_bankAccountFilter.TrySpendMoney(Settings.Price))
                                    return;

                                var defaultPopup = (DefaultPopupView)popup;
                                _selectedSpaTreatmentsType = defaultPopup.Dropdowns[0].GetCurrentValue<SpaTreatmentsType>();

                                EndActivity(true, false);
                            },
                            MoneySignState = true
                        }
                    },
                    UseIcon = true
                })
            });
        }

        protected override void EndActivity(bool useIcon, bool usePetIcon)
        {
            var spaTreatmentsType = Settings.Localization.GetValueTypeContent(_selectedSpaTreatmentsType);

            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<ResultPopup>(new ResultPopup()
                {
                    Title = Settings.Localization.Title,
                    Icon = Icon,
                    Content = string.Format(Settings.Localization.ResultContent, spaTreatmentsType.ToLower()),
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


        public enum SpaTreatmentsType
        {
            Massage = 0,
            Wrapping = 1,
            FaceMask = 2,
        }
    }
}