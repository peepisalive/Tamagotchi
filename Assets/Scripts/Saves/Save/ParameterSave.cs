using System;
using Core;

namespace Save
{
    [Serializable]
    public sealed class ParameterSave
    {
        public ParameterType Type;

        public float PreviousValue;
        public float Value;

        public float MaxValue;
        public float MinValue;
    }
}