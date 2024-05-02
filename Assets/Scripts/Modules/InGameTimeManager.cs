using Application = Tamagotchi.Application;
using System.Collections;
using UnityEngine;
using Save.State;
using System;
using Core;

namespace Modules
{
    public sealed class InGameTimeManager : MonoBehaviourSingleton<InGameTimeManager>, IStateLoadable
    {
        [field: SerializeField] public float PlayTimeSeconds { get; private set; }

        public void LoadState()
        {
            var stateHolder = Application.SaveDataManager.GetStateHolder<GlobalStateHolder>();

            if (stateHolder == null)
                return;

            PlayTimeSeconds = stateHolder.State.PlayTimeSeconds;
        }

        public void StartRecoveryCoroutine(float seconds, Action recoveryCallback)
        {
            StartCoroutine(RecoveryRoutine(seconds, recoveryCallback));
        }

        private IEnumerator RecoveryRoutine(float seconds, Action recoveryCallback)
        {
            yield return new WaitForSecondsRealtime(seconds);
            recoveryCallback?.Invoke();
            yield break;
        }

        private void Update()
        {
            PlayTimeSeconds += Time.deltaTime;
        }

        private void Awake()
        {
            Instance = this;
            LoadState();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}