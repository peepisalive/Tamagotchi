using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Settings.Activities
{
    [CreateAssetMenu(fileName = "ActivitiesSettings", menuName = "Settings/Activities/ActivitiesSettings", order = 0)]
    public sealed class ActivitiesSettings : ScriptableObject
    {
        [SerializeField] private List<ActivitySettings> _settings;

        public T Get<T>() where T : ActivitySettings
        {
#if UNITY_EDITOR
            if (!_settings.Any(s => s is T))
                Debug.LogError($"Not found settings [{nameof(T)}]");
#endif
            return (T)_settings.FirstOrDefault(s => s is T);
        }

        public void Add(ActivitySettings settings)
        {
            if (_settings.Contains(settings))
                return;

            _settings.Add(settings);
        }
    }
}