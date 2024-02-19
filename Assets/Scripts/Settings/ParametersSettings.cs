using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Core;

namespace Settings
{
    [CreateAssetMenu(fileName = "ParametersSettings", menuName = "Settings/ParametersSettings", order = 1)]
    public sealed class ParametersSettings : ScriptableObject
    {
        [SerializeField] private List<ParameterIcon> _icons;

        public Sprite GetIcon(ParameterType type)
        {
            return _icons.First(x => x.Type == type).Icon;
        }


        [Serializable]
        public sealed class ParameterIcon
        {
            [field: SerializeField] public ParameterType Type { get; private set; }
            [field: SerializeField] public Sprite Icon { get; private set; }
        }
    }
}