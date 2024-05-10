using System.Collections.Generic;
using Modules.Navigation;
using UnityEngine;
using System.Linq;
using System;

namespace Settings.Modules.Navigations
{
    [CreateAssetMenu(fileName = "NavigationSettings", menuName = "Settings/Modules/Navigation/NavigationSettings", order = 0)]
    public sealed class NavigationSettings : ScriptableObject
    {
        [field: SerializeField] public List<NavigationSet> Sets { get; private set; }
        [SerializeField] private List<NavigationStateIcon> _stateIcons;

        public bool TryGetSet(NavigationBlockType type, out NavigationSet set)
        {
            set = Sets.FirstOrDefault(x => x.BlockType == type);

            return set != null;
        }

        public Sprite GetStateIcon(NavigationButtonState state)
        {
            return _stateIcons.First(x => x.State == state).Icon;
        }
    }


    [Serializable]
    public sealed class NavigationSet
    {
        [field: SerializeField] public NavigationBlockType BlockType { get; private set; }
        [field: SerializeField] public NavigationElementType RootElementType { get; private set; }

        [field: SerializeField] public NavigationElementsSet ElementsSet { get; private set; }
    }


    [Serializable]
    public sealed class NavigationStateIcon
    {
        [field: SerializeField] public NavigationButtonState State { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}