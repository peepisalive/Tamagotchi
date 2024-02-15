using Modules.Navigation;
using UnityEngine;

namespace Settings.Activities
{
    public abstract class ActivitySettings : ScriptableObject
    {
        public abstract NavigationElementType Type { get; }
    }
}