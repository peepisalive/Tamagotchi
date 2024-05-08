using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Settings
{
    [CreateAssetMenu(fileName = "AccessoriesSettings", menuName = "Settings/AccessoriesSettings", order = 0)]
    public sealed class AccessoriesSettings : ScriptableObject
    {
        [SerializeField] private List<Accessory> _accessories;

        public Accessory GetAccessory(int index)
        {
            return _accessories[index];
        }
    }
}