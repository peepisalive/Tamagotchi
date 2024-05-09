using UnityEngine;
using System;

namespace Core
{
    [Serializable]
    public sealed class Accessory : AccessElement
    {
        [field: SerializeField] public AccessoryType Type { get; private set; }
        public GameObject Model { get; private set; }

        public Accessory(AccessoryType type, AccessType accessType, bool isUnlocked, bool isCurrent) : base(accessType, isUnlocked, isCurrent)
        {
            Type = type;
        }

        public void SetModel(GameObject model)
        {
            if (Model != null)
                return;

            Model = model;
        }
    }
}