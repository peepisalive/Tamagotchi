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

        public T Get<T>(NavigationElementType type) where T : ActivitySettings
        {
#if UNITY_EDITOR
            if (!_settings.Any(s => s.Type == type))
                Debug.LogError($"Not found settings by [{type}] type");
#endif
            return (T)_settings.FirstOrDefault(s => s.Type == type);
        }

        public void Add(ActivitySettings settings)
        {
            if (_settings.Contains(settings))
                return;

            _settings.Add(settings);
        }
    }
}