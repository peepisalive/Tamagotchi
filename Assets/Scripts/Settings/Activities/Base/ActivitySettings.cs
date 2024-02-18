using Modules.Localization;
using Modules.Navigation;
using UnityEngine;
using System;

namespace Settings.Activity
{
    public abstract class ActivitySettings : ScriptableObject
    {
        [field: SerializeField] public ActivityLocalization Localization { get; private set; }
        public abstract NavigationElementType Type { get; }
    }


    [Serializable]
    public sealed class ActivityLocalization
    {
        public string Title => LocalizationProvider.GetText("title");

        public string MainContent => LocalizationProvider.GetText("main_popup/content");
        public string ResultContent => LocalizationProvider.GetText("result_popup/content");

        public string LeftButtonContent => LocalizationProvider.GetText("button_content/left");
        public string RightButtonContent => LocalizationProvider.GetText("button_content/right");


        [SerializeField] private LocalizedText _asset;
    }
}