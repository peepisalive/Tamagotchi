using UnityEngine.UI;
using UnityEngine;
using Modules;
using Core;

namespace UI.Controller
{
    [RequireComponent(typeof(Button))]
    public sealed class SoundController : MonoBehaviour
    {
        [SerializeField] private SoundType _soundType;
        [SerializeField] private Button _button;

        private void OnClick()
        {
            if (!_button.interactable)
                return;

            SoundProvider.Instance.PlaySoundEffect(_soundType);
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}