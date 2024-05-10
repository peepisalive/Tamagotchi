using System.Collections.Generic;
using UI.Settings;
using UnityEngine;

namespace UI.Popups
{
    public class Popup
    {
        public string Title;
        public string Content;

        public bool UsePetIcon;
        public bool UseIcon;

        public bool IgnoreOverlayButton;

        public Sprite Icon;

        public List<TextButtonSettings> ButtonSettings;
    }

    public sealed class DefaultPopup : Popup
    {
        public List<DropdownSettings> DropdownSettings;
    }

    public sealed class ResultPopup : Popup
    {
        public List<InfoParameterSettings> InfoParameterSettings;

        public bool IsCloseButtonEnable;

        public ResultPopup()
        {
            UsePetIcon = true;
        }
    }
}