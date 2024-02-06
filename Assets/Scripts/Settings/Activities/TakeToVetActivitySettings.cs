using Modules.Navigation;
using UnityEngine;

namespace Settings.Activities
{
    [CreateAssetMenu(fileName = "TakeToVetActivitySettings", menuName = "Settings/Activities/TakeToVetActivitySettings", order = 1)]
    public sealed class TakeToVetActivitySettings : ActivitySettings
    {
        public override NavigationElementType Type => NavigationElementType.TakeToVetActivity;
    }
}