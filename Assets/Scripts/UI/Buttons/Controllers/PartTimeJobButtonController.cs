using Application = Tamagotchi.Application;
using System.Collections.Generic;
using Events.Popups;
using Settings.Job;
using UI.Settings;
using UI.Popups;
using Core.Job;
using Settings;
using Modules;
using Core;

namespace UI.Controller
{
    public sealed class PartTimeJobButtonController : JobButtonController, IAdsRewardable
    {
        public override void Setup(Job job, UnityEngine.Sprite icon, string title, string content)
        {
            base.Setup(job, icon, title, content);
            (View as PartTimeJobButtonView).SetAdsState(GetAdsSignState());
        }

        public void OnAdFailedToShow()
        {
            // to do
        }

        public void OnRewarded()
        {
            // to do
        }

        protected override void OnClick()
        {
            var adsSignState = GetAdsSignState();

            EventSystem.Send(new ShowPopupEvent
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = Title,
                    DropdownSettings = GetDropdownSettings(),
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Action = () =>
                            {
                                EventSystem.Send(new HidePopupEvent());
                            }
                        },
                        new TextButtonSettings
                        {
                            Action = () =>
                            {
                                if (adsSignState)
                                {
                                    RewardedAdManager.Instance.OnAdFailedToShowCallback += OnAdFailedToShow;
                                    RewardedAdManager.Instance.OnRewardedCallback += OnRewarded;
                                    RewardedAdManager.Instance.ShowRewardedAd();
                                }
                                else
                                {
                                    OnRewarded();
                                }
                            },
                            AdsSignState = adsSignState
                        }
                    }
                })
            });
        }

        private bool GetAdsSignState()
        {
            var currentAmount = Application.Model.GetJobPartTimeJobAmountPerDay();
            var amount = SettingsProvider.Get<JobSettings>().PartTimeJobAmountPerDay;

            return currentAmount == amount - 1;
        }
    }
}