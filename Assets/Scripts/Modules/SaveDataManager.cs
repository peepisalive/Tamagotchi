using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System;
using Events;
using Utils;
using Save;

namespace Modules
{
    public sealed class SaveDataManager
    {
        private Dictionary<Type, IStateHolder> _stateHolders;
        private SaveDataProvider _provider;

        private const string FILE_EXTENTION = "save";

        public SaveDataManager()
        {
            _stateHolders = new Dictionary<Type, IStateHolder>();
            _provider = new SaveDataProvider();

            InitializeStateHolders();
        }

        public T GetStateHolder<T>() where T : IStateHolder
        {
            return (T) _stateHolders[typeof(T)];
        }

        #region Clear saves
        [MenuItem("Tamagotchi/Clear saves")]
        public static void ClearSaves()
        {
            foreach (var savePath in Directory.EnumerateDirectories(SaveUtils.RootPath))
            {
                SaveUtils.DeleteDirectory(savePath);
            }
        }
        #endregion

        #region Save
        public void SaveData(Type type, bool isAsync)
        {
            if (_stateHolders.TryGetValue(type, out var stateHolder))
            {
                EventSystem.Send(new SaveDataEvent
                {
                    SaveData = new List<SaveData>
                    {
                        new SaveData
                        {
                            Path = GetFilePath(stateHolder.Id),
                            Data = stateHolder.StateToString()
                        }
                    },
                    IsAsync = isAsync
                });
            }
        }

        public void SaveData(bool isAsync)
        {
            var saveData = new List<SaveData>();

            foreach (var stateHolder in _stateHolders)
            {
                saveData.Add(new SaveData
                {
                    Path = GetFilePath(stateHolder.Value.Id),
                    Data = stateHolder.Value.StateToString()
                });
            }

            EventSystem.Send(new SaveDataEvent
            {
                SaveData = saveData,
                IsAsync = isAsync
            });
        }
        #endregion

        #region Load
        public bool TryLoadData(Type type)
        {
            if (_stateHolders.TryGetValue(type, out var stateHolder))
            {
                if (TryLoadFile(GetFilePath(stateHolder.Id), out var loadedData))
                    stateHolder.RestoreState(loadedData);

                return true;
            }

            return false;

            bool TryLoadFile(string filePath, out string loadedData)
            {
                loadedData = string.Empty;

                if (!File.Exists(filePath))
                    return false;

                return _provider.TryLoadFile(filePath, out loadedData);
            }
        }

        public void TryLoadData()
        {
            foreach (var stateHolder in _stateHolders)
            {
                TryLoadData(stateHolder.Key);
            }
        }
        #endregion

        private string GetFilePath(string fileName)
        {
            return Path.Combine(SaveUtils.RootPath, $"{fileName}.{FILE_EXTENTION}");
        }

        private void InitializeStateHolders()
        {
            _stateHolders.Add(typeof(PetStateHolder), new PetStateHolder());
        }
    }
}