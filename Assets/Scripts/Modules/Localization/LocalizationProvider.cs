using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using Settings.Modules.Localization;
using System.Collections.Generic;
using UnityEngine.Localization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;
using Settings;

namespace Modules.Localization
{
    public static class LocalizationProvider
    {
        public static LanguageType CurrentLanguage { get; private set; }

        private static Dictionary<string, LocalizationFileData> _localizationFiles;
        private static LocalizedText _navigationLocalizedText;
        private static LocalizedText _defaultLocalizedText;

        static LocalizationProvider()
        {
            var localizationSettings = SettingsProvider.Get<LocalizationDefaultSettings>();

            _localizationFiles = new Dictionary<string, LocalizationFileData>();
            _defaultLocalizedText = localizationSettings.DefaultLocalizationFile;
            _navigationLocalizedText = localizationSettings.NavigationLocalizationFile;
        }

        public static string GetText(LocalizedText asset, string tags)
        {
            var text = string.Empty;

            var entryName = asset.TableReference.TableCollectionName;
            var entryId = asset.TableEntryReference.KeyId;

            if (_localizationFiles.TryGetValue(GetFileDataKey(entryName, entryId), out var fileData))
                fileData.TryGetValue(tags, out text);

            return text;
        }

        public static string GetText(string tags)
        {
            var text = string.Empty;

            var entryName = _defaultLocalizedText.TableReference.TableCollectionName;
            var entryId = _defaultLocalizedText.TableEntryReference.KeyId;

            if (_localizationFiles.TryGetValue(GetFileDataKey(entryName, entryId), out var fileData))
                fileData.TryGetValue(tags, out text);

            return text;
        }

        public static string GetNavigationText(string tags)
        {
            var text = string.Empty;

            var entryName = _navigationLocalizedText.TableReference.TableCollectionName;
            var entryId = _navigationLocalizedText.TableEntryReference.KeyId;

            if (_localizationFiles.TryGetValue(GetFileDataKey(entryName, entryId), out var fileData))
                fileData.TryGetValue(tags, out text);

            return text;
        }

        public static string GetLocaleCode(LanguageType type)
        {
            return type switch
            {
                LanguageType.English => "en",
                LanguageType.Russia => "ru",
                _ => string.Empty
            };
        }

        public static LanguageType GetLanguageType(string localeCode)
        {
            return localeCode switch
            {
                "en" => LanguageType.English,
                "ru" => LanguageType.Russia,
                _ => LanguageType.English,
            };
        }

        public static async Task Initialize(Locale locale)
        {
            await Setup(locale);
        }

        private static async Task Setup(Locale locale)
        {
            if (_localizationFiles.Any())
                _localizationFiles.Clear();

            CurrentLanguage = GetLanguageType(locale.Identifier.Code);

            await LoadLocalization(locale);
        }

        private static async Task LoadLocalization(Locale locale)
        {
            var tables = await LocalizationSettings.AssetDatabase.GetAllTables().Task;
            var tableEntries = tables.SelectMany(t => t.Values);
            var tasks = new List<Task>();

            foreach (var entry in tableEntries)
            {
                tasks.Add(LoadLocalizationFile(entry, locale));
            }

            await Task.WhenAll(tasks);
        }

        private static async Task LoadLocalizationFile(AssetTableEntry entry, Locale locale)
        {
            var entryName = entry.Table.TableCollectionName;
            var entryId = entry.KeyId;
            var asset = await LocalizationSettings.AssetDatabase
                .GetLocalizedAssetAsync<TextAsset>(entryName, entryId, locale, FallbackBehavior.UseFallback).Task;

            var fileData = GetFileData(asset);
            var fileKey = GetFileDataKey(entryName, entryId);

            if (_localizationFiles.ContainsKey(fileKey))
                return;

            _localizationFiles.Add(fileKey, fileData);
        }

        private static LocalizationFileData GetFileData(TextAsset asset)
        {
            var data = JsonConvert.DeserializeObject<IEnumerable<LocalizationItem>>(asset.text);
            var loadedData = new LocalizationFileData();

            foreach (var item in data)
            {
                var key = string.Join('/', item.Tags);

                if (loadedData.ContainsKey(key))
                    continue;

                loadedData.Add(key, item.Topic);
            }

            return loadedData;
        }

        private static string GetFileDataKey(string entryName, long entryId)
        {
            return string.Join('/', new string[] { entryName, entryId.ToString() });
        }
    }
}