using UnityEngine;
using System;
using Core;

namespace Save
{
    [Serializable]
    public sealed class AccessorySave
    {
        public AccessoryType Type;
        public ColorSave Color;

        public bool IsUnlocked;
        public bool IsCurrent;
    }


    [Serializable]
    public sealed class ColorSave
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public ColorSave(Color color)
        {
            R = color.r;
            G = color.g;
            B = color.b;
            A = color.a;
        }

        public Color GetColor()
        {
            return new Color(R, G, B, A);
        }
    }
}