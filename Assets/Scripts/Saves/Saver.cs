using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;
using Events.Saves;
using System.Linq;
using UnityEngine;
using Modules;

namespace Save
{
    public sealed class Saver : MonoBehaviour
    {
        private Queue<List<SaveData>> _saveDataQueue;
        private List<SaveData> _currentSaveData;
        private CancellationTokenSource _cts;
        private SaveDataProvider _provider;
        private Task _currentTask;

        private bool _isSavingRoutine;

        private void HandleSaveDataRequest(SaveDataEvent e)
        {
            if (_saveDataQueue == null)
            {
                _saveDataQueue = new Queue<List<SaveData>>();
                _provider = new SaveDataProvider();
            }

            _saveDataQueue.Enqueue(e.SaveData);

            if (e.IsAsync)
            {
                if (!_isSavingRoutine)
                {
                    _isSavingRoutine = true;
                    StartCoroutine(nameof(SaveDataAsyncRoutine));
                }

                return;
            }
            else
            {
                if (_isSavingRoutine)
                {
                    StopCoroutine(nameof(SaveDataAsyncRoutine));
                    _isSavingRoutine = false;
                }

                SaveData();
            }
        }

        private IEnumerator SaveDataAsyncRoutine()
        {
            if (_cts == null)
                _cts = new CancellationTokenSource();

            while (_saveDataQueue.Any())
            {
                _currentSaveData = _saveDataQueue.Dequeue();
                _currentTask = Task.WhenAll(_currentSaveData.Select(sd => _provider.SaveFileAsync(sd.Path, sd.Data, _cts.Token)));

                yield return new WaitUntil(() => _currentTask.IsCompleted);
            }

            _isSavingRoutine = false;
            Debug.Log("All data saved");
        }

        private void SaveData()
        {
            if (_currentTask != null && !_currentTask.IsCompleted)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = new CancellationTokenSource();

                _currentSaveData.ForEach(sd =>
                {
                    _provider.SaveFile(sd.Path, sd.Data);
                });

                _currentTask = null;
                _currentSaveData = null;
            }

            if (_currentTask != null && _currentTask.IsCompleted)
                _currentTask.Dispose();

            while (_saveDataQueue.Any())
            {
                _saveDataQueue.Dequeue().ForEach(sd =>
                {
                    _provider.SaveFile(sd.Path, sd.Data);
                });
            }

            Debug.Log("All data saved");
        }

        private void Start()
        {
            EventSystem.Subscribe<SaveDataEvent>(HandleSaveDataRequest);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<SaveDataEvent>(HandleSaveDataRequest);
        }
    }


    public sealed class SaveData
    {
        public string Path;
        public string Data;
    }
}