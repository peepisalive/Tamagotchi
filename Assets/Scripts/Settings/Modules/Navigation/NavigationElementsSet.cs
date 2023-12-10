using System.Collections.Generic;
using Modules.Navigation;
using System.Linq;
using UnityEngine;

namespace Settings.Modules.Navigations
{
    [CreateAssetMenu(fileName = "NavigationElementsSet", menuName = "Settings/Navigation/NavigationElementsSet", order = 0)]
    public sealed class NavigationElementsSet : ScriptableObject
    {
        [field: SerializeField] public List<NavigationElementSettings> NavigationElementSettings { get; private set; }
        private Dictionary<NavigationElementType, NavigationElementSettings> _navigationElementSettings;

        public bool TryGetElementSettings(NavigationElementType type, out NavigationElementSettings elementSettings)
        {
            elementSettings = null;

            if (_navigationElementSettings == null)
                return false;

            return _navigationElementSettings.TryGetValue(type, out elementSettings);
        }

        private void OnEnable()
        {
            if (_navigationElementSettings != null && _navigationElementSettings.Count > 0)
                return;

            _navigationElementSettings = NavigationElementSettings.ToDictionary(x => x.Type);
        }
    }
}