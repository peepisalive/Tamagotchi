using System.Collections.Generic;
using Modules.Localization;
using UI.Settings;
using Components;
using Tamagotchi;
using UI.Popups;

namespace Utils
{
    public static class PopupUtils
    {
        public static void ShowNotEnoughMoneyPopup()
        {
            Application.Model.Send(new ShowPopup
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = LocalizationProvider.GetText("popup_not_enough_money/title"),
                    Content = LocalizationProvider.GetText("popup_not_enough_money/content"),
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Title = LocalizationProvider.GetText("ok/button"),
                            Action = () =>
                            {
                                Application.Model.Send(new HidePopup());
                            }
                        }
                    }
                })
            });
        }
    }
}