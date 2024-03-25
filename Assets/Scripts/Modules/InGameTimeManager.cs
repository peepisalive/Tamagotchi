using Application = Tamagotchi.Application;
using UnityEngine;
using Save.State;
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

        private void Update()
        {
            PlayTimeSeconds += Time.deltaTime;
        }

        private void Awake()
        {
            Instance = this;
            LoadState();
        }
    }
}