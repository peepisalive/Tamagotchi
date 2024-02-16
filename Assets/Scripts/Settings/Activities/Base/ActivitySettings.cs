using Modules.Navigation;
using UnityEngine;

namespace Settings.Activity
{
    public abstract class ActivitySettings : ScriptableObject
    {
        public abstract NavigationElementType Type { get; }
    }
}