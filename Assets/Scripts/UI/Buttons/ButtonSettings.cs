using UnityEngine;
using System;

namespace UI.Settings
{
    public class ButtonSettings
    {
        public Action Action;
    }

    public sealed class TextButtonSettings : ButtonSettings
    {
        public string Title;
        public Color? Color = null;
    }

    public sealed class ImageButtonSettings : ButtonSettings
    {
        public Sprite Icon;
    }
}