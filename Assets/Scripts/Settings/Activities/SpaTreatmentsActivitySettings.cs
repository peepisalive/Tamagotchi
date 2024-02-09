using Modules.Navigation;
using UnityEngine;

namespace Settings.Activities
{
    [CreateAssetMenu(fileName = "SpaTreatmentsActivitySettings", menuName = "Settings/Activities/SpaTreatmentsActivitySettings", order = 1)]
    public sealed class SpaTreatmentsActivitySettings : ActivitySettings
    {
        public override NavigationElementType Type => NavigationElementType.SpaTreatmentsActivity;
    }
}