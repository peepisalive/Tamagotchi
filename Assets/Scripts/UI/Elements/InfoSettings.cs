using Core;
using UnityEngine;

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
        public bool IconState;

        public Sprite Icon;
    }
}
