using System.Collections.Generic;
using System;
using Save;

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

        public Parameters(List<ParameterSave> saves) : this()
        {
            saves.ForEach(save =>
            {
                _parameters.Add(save.Type, new Parameter(save));
            });
        }

        public Parameter Get(ParameterType type)
        {
            if (_parameters.ContainsKey(type))
                return _parameters[type];

            return null;
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

        public List<ParameterSave> GetSaves()
        {
            var saves = new List<ParameterSave>();

            foreach (var parameter in _parameters)
            {
                saves.Add(parameter.Value.GetSave(parameter.Key));
            }

            return saves;
        }
    }
}