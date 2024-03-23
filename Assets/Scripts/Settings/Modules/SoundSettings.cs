using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Core;

namespace Settings.Modules
{
    [CreateAssetMenu(fileName = "SoundSettings", menuName = "Settings/Modules/Sound/SoundSettings", order = 0)]
    public sealed class SoundSettings : ScriptableObject
    {
        [SerializeField] private List<Sound> _sounds;

        public AudioClip GetAudioClip(SoundType type)
        {
            var sound = _sounds.FirstOrDefault(s => s.Type == type);

            if (sound == null)
            {
#if UNITY_EDITOR
                Debug.LogError($"Not found audio clip by type: [{type}]");
#endif
                return null;
            }
            return sound.AudioClip;
        }


        [Serializable]
        private sealed class Sound
        {
            [field: SerializeField] public SoundType Type;
            [field: SerializeField] public AudioClip AudioClip;
        }
    }
}