using System.Collections;
using UnityEngine;
using Settings;
using Modules;
using Events;

namespace Core
{
    public sealed class ParametersChangingTimeCounter : MonoBehaviour
    {
        private ParametersSettings _settings;

        private IEnumerator ParametersChangingRoutine()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(30f);

                _settings.ParameterDecRanges.ForEach(parameter =>
                {
                    EventSystem.Send(new ChangeParameterEvent
                    {
                        Type = parameter.Type,
                        Value = parameter.Range.GetRandom()
                    });
                });
            }
        }

        private void Awake()
        {
            _settings = SettingsProvider.Get<ParametersSettings>();
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