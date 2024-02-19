using UnityEngine;
using System;

namespace Core
{
    [Serializable]
    public sealed class ParameterRange
    {
        [field: SerializeField] public ParameterType Type { get; private set; }
        [field: SerializeField] public FloatRange Range { get; private set; }
    }
}