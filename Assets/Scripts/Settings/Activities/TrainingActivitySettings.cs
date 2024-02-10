using Modules.Navigation;
using UnityEngine;

namespace Settings.Activities
{
    [CreateAssetMenu(fileName = "TrainingActivitySettings", menuName = "Settings/Activities/TrainingActivitySettings", order = 1)]
    public class TrainingActivitySettings : ActivitySettings
    {
        public override NavigationElementType Type => NavigationElementType.TrainingActivity;
    }
}