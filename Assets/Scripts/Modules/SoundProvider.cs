using Application = Tamagotchi.Application;
using Settings.Modules;
using UnityEngine;
using Extensions;
using Save.State;
using Settings;
using Core;

namespace Modules
{
    public sealed class SoundProvider : MonoBehaviourSingleton<SoundProvider>, IStateLoadable
    {
#if UNITY_EDITOR
        [field: ReadOnly]
#endif
        [field: SerializeField] public bool State { get; private set; } = true;

        private AudioSource _audioSource;
        private SoundSettings _settings;

        public void PlaySoundEffect(SoundType type, int volume = 100)
        {
            if (!State)
                return;

            var clip = _settings.GetAudioClip(type);

            if (clip == null)
                return;

            _audioSource.volume = volume;
            _audioSource.PlayOneShot(clip);
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

            State = stateHolder.State.SoundState;
        }

        private void Start()
        {
            Instance = this;

            _audioSource = GetComponentInChildren<AudioSource>();
            _settings = SettingsProvider.Get<SoundSettings>();

            LoadState();
        }
    }
}