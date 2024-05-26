using UnityEngine;
using Core;

namespace UI.Settings
{
    public sealed class InfoParameterSettings
    {
        public ParameterType Type;
        public float Value;
    }


    public sealed class InfoFieldSettings
    {
        public string Title;
        public string Content;

        public bool IconState;
        public Sprite Icon;
    }
}
