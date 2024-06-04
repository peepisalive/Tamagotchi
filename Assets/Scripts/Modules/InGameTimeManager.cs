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
        public event Action<int> OnCountRemainingTimeEvent;

        [field: SerializeField] public float TotalPlayTimeSeconds { get; private set; }
        [field: SerializeField] public float LastSessionPlayTimeSeconds { get; private set; }

        [field: SerializeField] public int RemainingSeconds { get; private set; }

        public void LoadState()
        {
            var stateHolder = Application.SaveDataManager.GetStateHolder<GlobalStateHolder>();

            if (stateHolder == null)
                return;

            TotalPlayTimeSeconds = stateHolder.State.TotalPlayTimeSeconds;
        }

        public void StartRecoveryCoroutine(float seconds, Action recoveryCallback)
        {
            StartCoroutine(RecoveryRoutine(seconds, recoveryCallback));


            static IEnumerator RecoveryRoutine(float seconds, Action recoveryCallback)
            {
                yield return new WaitForSecondsRealtime(seconds);
                recoveryCallback?.Invoke();
                yield break;
            }
        }

        public void StartCountRemainingTimeRoutine(int seconds)
        {
            RemainingSeconds = seconds;
            StartCoroutine(CountRoutine());


            IEnumerator CountRoutine()
            {
                while (RemainingSeconds > 0)
                {
                    yield return new WaitForSecondsRealtime(1f);

                    RemainingSeconds -= 1;
                    OnCountRemainingTimeEvent?.Invoke(RemainingSeconds);
                }
                
                yield break;
            }
        }

        private void Update()
        {
            TotalPlayTimeSeconds += Time.deltaTime;
            LastSessionPlayTimeSeconds += Time.deltaTime;
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