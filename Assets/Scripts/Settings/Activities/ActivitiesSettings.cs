using System.Collections.Generic;
using Modules.Navigation;
using System.Linq;
using UnityEngine;

namespace Settings.Activity
{
    [CreateAssetMenu(fileName = "ActivitiesSettings", menuName = "Settings/Activities/ActivitiesSettings", order = 0)]
    public sealed class ActivitiesSettings : ScriptableObject
    {
        [SerializeField] private List<ActivitySettings> _settings;

        public T Get<T>(NavigationElementType type) where T : ActivitySettings
        {
            var settings = (T)_settings.FirstOrDefault(s => s.Type == type);
#if UNITY_EDITOR
            if (settings == null)
                Debug.LogError($"Not found settings by type: [{type}]");
#endif
            return settings;
        }

#if UNITY_EDITOR
        public void Add(ActivitySettings settings)
        {
            if (_settings.Contains(settings))
                return;

            _settings.Add(settings);
        }
#endif
    }
}