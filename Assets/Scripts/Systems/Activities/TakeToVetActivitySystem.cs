using System.Collections.Generic;
using Settings.Activity;
using Modules.Navigation;
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
                            Action = () =>
                            {
                                //if (!_bankAccountFilter.IsMoneyAvailable(Settings.Price))
                                //{
                                //    PopupUtils.ShowNotEnoughMoneyPopup();
                                //    return;
                                //}

                                World.NewEntity().Replace(new ChangeBankAccountValueEvent
                                {
                                    Value = Settings.Price
                                });

                                EndActivity();
                            }
                        }
                    }
                })
            });
        }

        private void EndActivity()
        {
            World.NewEntity().Replace(new ShowPopup
            {
                Settings = new PopupToShow<ResultPopup>(new ResultPopup
                {
                    Title = Settings.Localization.Title,
                    Content = Settings.Localization.ResultContent,
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