using Modules.Navigation;
using UnityEngine;

namespace Settings.Activities
{
    [CreateAssetMenu(fileName = "FeedActivitySettings", menuName = "Settings/Activities/FeedActivitySettings", order = 1)]
    public class FeedActivitySettings : ActivitySettings
    {
        public override NavigationElementType Type => NavigationElementType.FeedActivity;
    }
}