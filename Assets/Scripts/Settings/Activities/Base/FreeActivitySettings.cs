using Modules.Navigation;
using UnityEngine;

namespace Settings.Activity
{
    [CreateAssetMenu(fileName = "FreeActivitySettings", menuName = "Settings/Activities/FreeActivitySettings", order = 1)]
    public sealed class FreeActivitySettings : ActivitySettings
    {
        public override NavigationElementType Type => _type;

        [SerializeField] private NavigationElementType _type;
    }
}