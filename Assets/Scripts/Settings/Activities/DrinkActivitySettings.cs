using Modules.Navigation;
using UnityEngine;

namespace Settings.Activities
{
    [CreateAssetMenu(fileName = "DrinkActivitySettings", menuName = "Settings/Activities/DrinkActivitySettings", order = 1)]
    public class DrinkActivitySettings : ActivitySettings
    {
        public override NavigationElementType Type => NavigationElementType.DrinkActivity;
    }
}