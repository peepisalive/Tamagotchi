using Modules.Navigation;
using UnityEngine;

namespace Settings.Activities
{
    [CreateAssetMenu(fileName = "BallGameActivitySettings", menuName = "Settings/Activities/BallGameActivitySettings", order = 1)]
    public class BallGameActivitySettings : ActivitySettings
    {
        public override NavigationElementType Type => NavigationElementType.BallGameActivity;
    }
}