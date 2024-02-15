using Modules.Navigation;
using UnityEngine;

namespace Settings.Activities
{
    [CreateAssetMenu(fileName = "WashActivitySettings", menuName = "Settings/Activities/WashActivitySettings", order = 1)]
    public class WashActivitySettings : ActivitySettings
    {
        public override NavigationElementType Type => NavigationElementType.WashActivity;
    }
}