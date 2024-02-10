using Modules.Localization;
using UnityEngine;

namespace Settings.Modules.Localization
{
    [CreateAssetMenu(fileName = "LocalizationDefaultSettings", menuName = "Settings/Modules/Localization/LocalizationDefaultSettings", order = 0)]
    public sealed class LocalizationDefaultSettings : ScriptableObject
    {
        [field: SerializeField] public LocalizedText DefaultLocalizationFile { get; private set; }
        [field: SerializeField] public LocalizedText NavigationLocalizationFile { get; private set; }
    }
}