using System.Collections.Generic;
using Modules.Localization;
using Modules.Navigation;
using UnityEngine;

namespace Settings.Modules.Navigations
{
    [CreateAssetMenu(fileName = "NavigationElementSettings", menuName = "Settings/Modules/Navigation/NavigationElementSettings", order = 0)]
    public sealed class NavigationElementSettings : ScriptableObject
    {
        [field: SerializeField] public NavigationElementType Type { get; private set; }
        [field: SerializeField] public List<NavigationElementType> ChildTypes { get; private set; }

        private const string TITLE_KEY = "navigation_title_{0}";
        private const string DESCR_KEY = "navigation_descr_{0}";

        public NavigationButtonData GetNavigationButtonData()
        {
            return new NavigationButtonData
            {
                Type = Type,
                Title = LocalizationProvider.GetText(string.Format(TITLE_KEY, Type.ToString())),
                Description = LocalizationProvider.GetText(string.Format(DESCR_KEY, Type.ToString())),
            };
        }

        public NavigationScreenData GetNavigationScreenData()
        {
            return new NavigationScreenData
            {
                Type = Type,
                Title = LocalizationProvider.GetText(string.Format(TITLE_KEY, Type.ToString())),
            };
        }
    }
}