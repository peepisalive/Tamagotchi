using System.Collections.Generic;
using Modules.Navigation;
using UnityEngine;
using System.Linq;
using System;

namespace Settings.Modules.Navigations
{
    [CreateAssetMenu(fileName = "NavigationIconSettings", menuName = "Settings/Modules/Navigation/NavigationIconSettings", order = 0)]
    public sealed class NavigationIconSettings : ScriptableObject
    {
        [SerializeField] private List<NavigationIcon> _icons;

        public Sprite GetIcon(NavigationElementType type)
        {
            var icon = _icons.FirstOrDefault(navigationIcon => navigationIcon.Type == type)?.Icon;

#if UNITY_EDITOR
            if (icon == null)
                Debug.LogError($"Not found icon by type: [{type}]");
#endif

            return icon;
        }


        [Serializable]
        private sealed class NavigationIcon
        {
            [field: SerializeField] public NavigationElementType Type { get; private set; }
            [field: SerializeField] public Sprite Icon { get; private set; }
        }
    }
}