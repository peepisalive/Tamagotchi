using System.Collections.Generic;
using Modules.Navigation;
using Settings.Activity;
using Leopotam.Ecs;
using UI.Settings;
using Components;
using UI.Popups;

namespace Systems.Activities
{
    public sealed class TakeToVetActivitySystem : ActivitySystem<PaidActivitySettings>
    {
        protected override NavigationElementType Type => NavigationElementType.TakeToVetActivity;

        private EcsFilter<BankAccountComponent> _bankAccountFilter;
        private EcsFilter<PetComponent> _petFilter;

        protected override void StartActivity(bool isEnable)
        {
            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = Settings.Localization.Title,
                    Content = Settings.Localization.MainContent,
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
                                //if (!_bankAccountFilter.IsMoneyAvailable(Settings.Price))
                                //{
                                //    PopupUtils.ShowNotEnoughMoneyPopup();
                                //    return;
                                //}

                                //World.NewEntity().Replace(new ChangeBankAccountValueEvent
                                //{
                                //    Value = Settings.Price
                                //});

                                EndActivity();
                            }
                        }
                    }
                })
            });
        }

        private void EndActivity()
        {
            var infoParametersSettings = new List<InfoParameterSettings>();

            foreach (var i in _petFilter)
            {
                var pet = _petFilter.Get1(i).Pet;

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

            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<ResultPopup>(new ResultPopup()
                {
                    Title = Settings.Localization.Title,
                    Content = Settings.Localization.ResultContent,
                    InfoParameterSettings = infoParametersSettings,
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
                    }
                })
            });
        }
    }
}