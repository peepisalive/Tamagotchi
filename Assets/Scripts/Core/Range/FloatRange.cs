using UnityEngine;
using System;

namespace Core
{
    [Serializable]
    public sealed class FloatRange
    {
        [field: SerializeField] public float Min { get; private set; }
        [field: SerializeField] public float Max { get; private set; }

        public FloatRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float GetRandom()
        {
            return UnityEngine.Random.Range(Min, Max);
        }
    }
}