using System.Collections.Generic;
using UnityEngine;
using UI.Settings;

namespace UI
{
    public class Popup
    {
        public string Title;
        public Color? Color = null;
        public Vector3 Direction;
        public bool IgnoreOverlayButtonAction;
        public List<TextButtonSettings> ButtonSettings;
    }
}