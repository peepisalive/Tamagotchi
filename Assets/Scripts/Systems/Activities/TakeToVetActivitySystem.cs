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
    public sealed class TakeToVetActivitySystem : ActivitySystem<PaidActivitySettings>
    {
        protected override NavigationElementType Type => NavigationElementType.TakeToVetActivity;

        private EcsFilter<BankAccountComponent> _bankAccountFilter;
        private bool _isFullExamination;

        protected override void StartActivity(bool isEnable)
        {
            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = Settings.Localization.Title,
                    Icon = Icon,
                    Content = Settings.Localization.MainContent,
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Title = Settings.Localization.LeftButtonContent,
                            Action = () =>
                            {
                                if (!_bankAccountFilter.TrySpendMoney(Settings.Price / 2))
                                    return;

                                _isFullExamination = false;
                                EndActivity(true, false);
                            },
                            MoneySignState = true
                        },
                        new TextButtonSettings
                        {
                            Title = Settings.Localization.RightButtonContent,
                            Action = () =>
                            {
                                if (!_bankAccountFilter.TrySpendMoney(Settings.Price))
                                    return;

                                _isFullExamination = true;
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

        protected override List<InfoParameterSettings> GetInfoParameterSettings()
        {
            var infoParametersSettings = new List<InfoParameterSettings>();

            foreach (var i in PetFilter)
            {
                var pet = PetFilter.Get1(i).Pet;

                foreach (var parameterChange in Settings.ParametersChanges)
                {
                    var value = parameterChange.Range.GetRandom();

                    if (!_isFullExamination && value > 0f)
                        value /= 2f;

                    pet.Parameters.Get(parameterChange.Type).Add(value);
                    infoParametersSettings.Add(new InfoParameterSettings
                    {
                        Type = parameterChange.Type,
                        Value = pet.Parameters.Get(parameterChange.Type).Value
                    });
                }
            }

            return infoParametersSettings;
        }
    }
}