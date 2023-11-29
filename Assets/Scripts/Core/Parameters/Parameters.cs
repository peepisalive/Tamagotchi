using System.Collections.Generic;
using System;

namespace Core
{
    public sealed class Parameters
    {
        public event Action<ParameterType, float, float> OnParameterValueChanged;
        private Dictionary<ParameterType, Parameter> _parameters;

        public Parameters()
        {
            _parameters = new Dictionary<ParameterType, Parameter>();
        }

        public void Add(ParameterType type, Parameter parameter)
        {
            if (_parameters.ContainsKey(type))
                return;

            _parameters.Add(type, parameter);
            _parameters[type].OnValueChanged += (value, previousValue) =>
            {
                OnParameterValueChanged?.Invoke(type, value, previousValue);
            };
        }

        public void Remove(ParameterType type)
        {
            if (!_parameters.ContainsKey(type))
                return;

            _parameters.Remove(type);
            _parameters[type].OnValueChanged -= (value, previousValue) =>
            {
                OnParameterValueChanged?.Invoke(type, value, previousValue);
            };
        }
    }
}