using Application = Tamagotchi.Application;
using System.Collections.Generic;
using Events.Popups;
using Settings.Job;
using UnityEngine;
using UI.Settings;
using UI.Popups;
using Core.Job;
using Settings;
using Modules;
using Events;
using Utils;
using Core;

namespace UI.Controller
{
    public sealed class PartTimeJobButtonController : JobButtonController, IAdRewardable
    {
        private int _partTimeJobAmountPerDay;

        public override void Setup(Job job, Sprite icon, string title, string content)
        {
            base.Setup(job, icon, title, content);
            (View as PartTimeJobButtonView).SetAdState(GetAdsSignState());
        }

        public void OnAdFailedToShow()
        {
            PopupUtils.ShowNoAdsAvailablePopup();
        }

        public void OnRewarded()
        {
            EventSystem.Send(new GettingJobEvent(Job));
        }

        protected override void OnClick()
        {
            var adsSignState = GetAdsSignState();

            EventSystem.Send(new ShowPopupEvent
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = Title,
                    Icon = Icon,
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

                                EventSystem.Send(new HidePopupEvent());
                            },
                            AdsSignState = adsSignState
                        }
                    },
                    UseIcon = true
                })
            });
        }

        private bool GetAdsSignState()
        {
            if (_partTimeJobAmountPerDay == 0)
                _partTimeJobAmountPerDay = SettingsProvider.Get<JobSettings>().PartTimeAmountPerDay;

            return Application.Model.GetPartTimeAmountPerDay() == _partTimeJobAmountPerDay - 1;
        }
    }
}