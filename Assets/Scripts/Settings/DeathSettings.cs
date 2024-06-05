using System.Collections.Generic;
using Modules.Localization;
using Modules.Navigation;
using UnityEngine;
using System;

namespace Settings
{
    [CreateAssetMenu(fileName = "DeathSettings", menuName = "Settings/DeathSettings", order = 0)]
    public sealed class DeathSettings : ScriptableObject
    {
        [field: SerializeField] public DeathLocalization Localization { get; private set; }
        [field: SerializeField] public List<NavigationElementType> PetIconExcludingTypes { get; private set; }


        [Serializable]
        public sealed class DeathLocalization
        {
            public string PopupTitle => LocalizationProvider.GetText(_asset, "popup/title");
            public string PopupContent => LocalizationProvider.GetText(_asset, "popup/content");

            public string NewPetButtonTitle => LocalizationProvider.GetText(_asset, "left/button");
            public string ResurrectButtonTitle => LocalizationProvider.GetText(_asset, "right/button");
            public string DontCareButtonTitle => LocalizationProvider.GetText(_asset, "down/left/button");


            [SerializeField] private LocalizedText _asset;
        }
    }
}