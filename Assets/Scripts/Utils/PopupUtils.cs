using System.Collections.Generic;
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
                    Title = "Oops", // to do: edit this
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Title = "Ok", // to do: edit this
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