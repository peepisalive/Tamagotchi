using UnityEngine;
using System;

namespace Core
{
    [Serializable]
    public sealed class Accessory : AccessElement
    {
        [field: SerializeField] public AccessoryType Type { get; private set; }
        [field: SerializeField] public GameObject Model { get; private set; }
    }
}