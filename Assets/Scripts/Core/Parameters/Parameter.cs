using UnityEngine;
using System;

namespace Core
{
    public sealed class Parameter
    {
        public event Action<float, float> OnValueChanged;

        public ParameterType Type { get; private set; }
        public float PreviousValue { get; private set; }
        public float Value { get; private set; }

        private FloatRange ValueRange;

        public Parameter(ParameterType type, float value, FloatRange valueRange)
        {
            Type = type;
            Value = value;
            PreviousValue = 0f;
            ValueRange = valueRange;
        }

        public void Add(float value)
        {
            PreviousValue = Value;
            Value = Mathf.Clamp(Value + value, ValueRange.Min, ValueRange.Max);

            OnValueChanged?.Invoke(Value, PreviousValue);
        }

        public void Dec(float value)
        {
            PreviousValue = Value;
            Value = Mathf.Clamp(Value - value, ValueRange.Min, ValueRange.Max);

            OnValueChanged?.Invoke(Value, PreviousValue);
        }
    }
}