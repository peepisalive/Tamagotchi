using UnityEngine;
using System;

namespace Core
{
    public sealed class Parameter
    {
        public event Action<float, float> OnValueChanged;

        public float PreviousValue { get; private set; }
        public float Value { get; private set; }

        private FloatRange ValueRange;

        public Parameter(float value, FloatRange valueRange)
        {
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