using System.Collections.Generic;
using Modules.Navigation;
using System.Linq;
using UnityEngine;

namespace Settings.Activities
{
    [CreateAssetMenu(fileName = "ActivitiesSettings", menuName = "Settings/Activities/ActivitiesSettings", order = 0)]
    public sealed class ActivitiesSettings : ScriptableObject
    {
        [SerializeField] private List<ActivitySettings> _settings;

        public ActivitySettings Get(NavigationElementType type)
        {
#if UNITY_EDITOR
            if (!_settings.Any(s => s.Type == type))
                Debug.LogError($"Not found settings by [{type}] type");
#endif
            return _settings.FirstOrDefault(s => s.Type == type);
        }
    }
}