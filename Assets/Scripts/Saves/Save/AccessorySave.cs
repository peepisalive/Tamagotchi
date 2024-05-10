using Newtonsoft.Json;
using UnityEngine;
using System;
using Core;

namespace Save
{
    [Serializable]
    public sealed class AccessorySave
    {
        public AccessoryType Type;
        [JsonIgnore] public Color Color = Color.clear;

        public bool IsUnlocked;
        public bool IsCurrent;
    }
}