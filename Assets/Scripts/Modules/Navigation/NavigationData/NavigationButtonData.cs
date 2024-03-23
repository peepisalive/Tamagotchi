using UnityEngine;

namespace Modules.Navigation
{
    public sealed class NavigationButtonData
    {
        public NavigationElementType Type;
        public NavigationButtonState StateType;

        public Sprite Icon;

        public string Title;
        public string Description;

        public bool IsToggleButton;
    }
}