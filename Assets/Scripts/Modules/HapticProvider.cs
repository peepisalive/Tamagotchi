using Application = Tamagotchi.Application;
using MoreMountains.NiceVibrations;
using UnityEngine;
using Save.State;
using Extensions;
using Core;

namespace Modules
{
    public sealed class HapticProvider : MonoBehaviourSingleton<HapticProvider>, IStateLoadable
    {
#if UNITY_EDITOR
        [field: ReadOnly]
#endif
        [field: SerializeField] public bool State { get; private set; } = true;

        public void Haptic(HapticTypes type)
        {
#if UNITY_EDITOR
            return;
#endif
            if (!State)
                return;

            MMVibrationManager.Haptic(type);
        }

        public void SwitchState()
        {
            State = !State;
        }

        public void LoadState()
        {
            var stateHolder = Application.SaveDataManager.GetStateHolder<SettingsStateHolder>();

            if (stateHolder == null)
                return;

            State = stateHolder.State.HapticState;
        }

        private void Start()
        {
            Instance = this;

            LoadState();
        }
    }
}