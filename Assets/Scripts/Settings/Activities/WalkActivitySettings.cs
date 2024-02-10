using Modules.Navigation;
using UnityEngine;

namespace Settings.Activities
{
    [CreateAssetMenu(fileName = "WalkActivitySettings", menuName = "Settings/Activities/WalkActivitySettings", order = 1)]
    public class WalkActivitySettings : ActivitySettings
    {
        public override NavigationElementType Type => NavigationElementType.WalkActivity;
    }
}