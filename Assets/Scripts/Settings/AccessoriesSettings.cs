using System.Collections.Generic;
using Modules.Localization;
using UnityEngine;
using System.Linq;
using System;
using Core;

namespace Settings
{
    [CreateAssetMenu(fileName = "AccessoriesSettings", menuName = "Settings/AccessoriesSettings", order = 0)]
    public sealed class AccessoriesSettings : ScriptableObject
    {
        [field: SerializeField] public AccessoryLocalization Localization { get; private set; }
        [field: SerializeField] public List<AccessorySettings> Accessories { get; private set; }

        public AccessorySettings GetAccessory(AccessoryType type)
        {
            return Accessories.First(a => a.Type == type);
        }


        [Serializable]
        public sealed class AccessorySettings
        {
            [field: SerializeField] public AccessType AccessType { get; private set; }
            [field: SerializeField] public AccessoryType Type { get; private set; }
            [field: SerializeField][field: Range(-10000, 0)] public int Value { get; private set; }
        }


        [Serializable]
        public sealed class AccessoryLocalization
        {
            public string SaveChangesTitle => LocalizationProvider.GetText(_asset, "button/title/save_changes");


            [SerializeField] private LocalizedText _asset;

            public string GetAccessoryName(AccessoryType type)
            {
                return LocalizationProvider.GetText(_asset, $"name/{type}");
            }
        }
    }
}