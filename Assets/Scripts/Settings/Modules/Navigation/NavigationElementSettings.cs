using System.Collections.Generic;
using Modules.Navigation;
using UnityEngine;

namespace Settings.Modules.Navigations
{
    [CreateAssetMenu(fileName = "NavigationElementSettings", menuName = "Settings/Navigation/NavigationElementSettings", order = 0)]
    public sealed class NavigationElementSettings : ScriptableObject
    {
        [field: SerializeField] public NavigationElementType Type { get; private set; }
        [field: SerializeField] public List<NavigationElementType> ChildTypes { get; private set; }

        public NavigationButtonData GetNavigationButtonData()
        {
            return new NavigationButtonData();
        }
    }
}