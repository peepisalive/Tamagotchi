using UnityEngine;
using UI.View;
using Core;

namespace UI.Controller
{
    [RequireComponent(typeof(ParameterBarView))]
    public class ParameterBarController : MonoBehaviour
    {
        [field: SerializeField] public ParameterType Type { get; private set; }

        [SerializeField] private ParameterBarView _view;
        private Parameter _parameter;

        public void Setup(Parameter parameter)
        {
            _parameter = parameter;

            _view.SetFillValue(_parameter.Value, _parameter.PreviousValue);
            _parameter.OnValueChanged += _view.SetFillValue;
        }

        private void OnDestroy()
        {
            _parameter.OnValueChanged -= _view.SetFillValue;
        }
    }
}