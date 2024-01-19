using UnityEngine;
using System;
using Save;

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

        public Parameter(ParameterSave save)
        {
            ValueRange = new FloatRange(save.MinValue, save.MaxValue);
            PreviousValue = save.PreviousValue;
            Value = save.Value;
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

        public ParameterSave GetSave(ParameterType type)
        {
            return new ParameterSave
            {
                Type = type,
                PreviousValue = PreviousValue,
                Value = Value,
                MaxValue = ValueRange.Max,
                MinValue = ValueRange.Min,
            };
        }
    }
}