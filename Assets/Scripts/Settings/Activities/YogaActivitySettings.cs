using Modules.Navigation;
using UnityEngine;

namespace Settings.Activities
{
    [CreateAssetMenu(fileName = "YogaActivitySettings", menuName = "Settings/Activities/YogaActivitySettings", order = 1)]
    public class YogaActivitySettings : ActivitySettings
    {
        public override NavigationElementType Type => NavigationElementType.YogaActivity;
    }
}