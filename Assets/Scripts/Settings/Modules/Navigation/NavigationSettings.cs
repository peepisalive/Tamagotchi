using System.Collections.Generic;
using Modules.Navigation;
using UnityEngine;
using System.Linq;
using System;

namespace Settings.Modules.Navigations
{
    [CreateAssetMenu(fileName = "NavigationSettings", menuName = "Settings/Navigation/NavigationSettings", order = 0)]
    public sealed class NavigationSettings : ScriptableObject
    {
        [field: SerializeField] public List<NavigationSet> Sets { get; private set; }

        public bool TryGetSet(NavigationBlockType type, out NavigationSet set)
        {
            set = Sets.FirstOrDefault(x => x.BlockType == type);

            return set != null;
        }
    }


    [Serializable]
    public sealed class NavigationSet
    {
        [field: SerializeField] public NavigationBlockType BlockType { get; private set; }
        [field: SerializeField] public NavigationElementType RootElementType { get; private set; }

        [field: SerializeField] public NavigationElementsSet ElementsSet { get; private set; }
    }
}