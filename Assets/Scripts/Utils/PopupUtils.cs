using System.Collections.Generic;
using Modules.Localization;
using Events.Popups;
using UI.Settings;
using UI.Popups;
using Modules;

namespace Utils
{
    public static class PopupUtils
    {
        public static void ShowNotEnoughMoneyPopup()
        {
            EventSystem.Send(new ShowPopupEvent
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = LocalizationProvider.GetText("oops/title"),
                    Content = LocalizationProvider.GetText("popup_not_enough_money/content"),
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Title = LocalizationProvider.GetText("ok/button"),
                            Action = () =>
                            {
                                EventSystem.Send(new HidePopupEvent());
                            }
                        }
                    }
                })
            });
        }

        public static void ShowNoAdsAvailablePopup()
        {
            EventSystem.Send(new ShowPopupEvent
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = LocalizationProvider.GetText("oops/title"),
                    Content = LocalizationProvider.GetText("popup_not_ads_available/content"),
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Title = LocalizationProvider.GetText("ok/button"),
                            Action = () =>
                            {
                                EventSystem.Send(new HidePopupEvent());
                            }
                        }
                    }
                })
            });
        }
    }
}