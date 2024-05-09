using System.Collections.Generic;
using Modules.Localization;
using UnityEngine;
using System;
using Core;

namespace Settings
{
    [CreateAssetMenu(fileName = "AccessoriesSettings", menuName = "Settings/AccessoriesSettings", order = 0)]
    public sealed class AccessoriesSettings : ScriptableObject
    {
        [field: SerializeField] public AccessoryLocalization Localization { get; private set; }
        [field: SerializeField] public List<Accessory> Accessories { get; private set; }


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