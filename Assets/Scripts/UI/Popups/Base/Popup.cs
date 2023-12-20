using System.Collections.Generic;
using UI.Settings;

namespace UI.Popups
{
    public class Popup
    {
        public bool IgnoreOverlayButtonAction;
        public List<TextButtonSettings> ButtonSettings;
    }

    public sealed class DefaultPopup : Popup
    {
        public string Title;
    }
}