using System.Collections;
using UnityEngine;
using Modules;
using Events;

namespace Core
{
    public sealed class ParametersChangingTimeCounter : MonoBehaviour
    {
        private IEnumerator ParametersChangingRoutine()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(5f); // to do: edit time
                EventSystem.Send(new ChangeParameterEvent());
            }
        }

        private void Awake()
        {

        }

        private void Start()
        {
            //StartCoroutine(nameof(ParametersChangingRoutine));
        }

        private void OnDestroy()
        {
            //StopCoroutine(nameof(ParametersChangingRoutine));
        }
    }
}