using UnityEngine.Localization.Metadata;
using System.ComponentModel;
using UnityEngine;
using System;

namespace Modules.Localization
{
    [Metadata(AllowedTypes = MetadataType.Locale)]
    [DisplayName("System language")]
    [Serializable]
    public sealed class SystemLanguageMetadata : IMetadata
    {
        public SystemLanguage Language;
    }
}