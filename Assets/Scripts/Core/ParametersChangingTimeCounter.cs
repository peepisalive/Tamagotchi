using System.Collections;
using UnityEngine;
using Settings;
using Modules;
using Events;

namespace Core
{
    public sealed class ParametersChangingTimeCounter : MonoBehaviour
    {
        private ParametersChangingSettings _settings;

        private IEnumerator ParametersChangingRoutine()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(5f); // edit time
                EventSystem.Send(new ChangeParametersEvent());
            }
        }

        private void Awake()
        {
            _settings = SettingsProvider.Get<ParametersChangingSettings>();
        }

        private void Start()
        {
            StartCoroutine(nameof(ParametersChangingRoutine));
        }

        private void OnDestroy()
        {
            StopCoroutine(nameof(ParametersChangingRoutine));
        }
    }
}