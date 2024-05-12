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
        public event Action<int> OnCountFullTimeJobTimeEvent;

        [field: SerializeField] public float TotalPlayTimeSeconds { get; private set; }
        [field: SerializeField] public float LastSessionPlayTimeSeconds { get; private set; }

        [field: SerializeField] public int FullTimeJobRemainingSeconds { get; private set; }

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

        public void StartCountFullTimeJobTimeRoutine(int seconds)
        {
            FullTimeJobRemainingSeconds = seconds;
            StartCoroutine(CountRoutine());


            IEnumerator CountRoutine()
            {
                while (FullTimeJobRemainingSeconds > 0)
                {
                    yield return new WaitForSecondsRealtime(1f);

                    FullTimeJobRemainingSeconds -= 1;
                    OnCountFullTimeJobTimeEvent?.Invoke(FullTimeJobRemainingSeconds);
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