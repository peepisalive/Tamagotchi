using UnityEngine;
using UI.View;
using Core;

namespace UI.Controller
{
    [RequireComponent(typeof(BarView))]
    public class ParameterBarController : BarController
    {
        [field: SerializeField] public ParameterType Type { get; private set; }

        private Parameter _parameter;

        public void Setup(Parameter parameter)
        {
            _parameter = parameter;

            SetValue(_parameter.Value, _parameter.PreviousValue);
            _parameter.OnValueChanged += SetValue;
        }

        private void OnDestroy()
        {
            _parameter.OnValueChanged -= SetValue;
        }
    }
}