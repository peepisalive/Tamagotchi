using MoreMountains.NiceVibrations;
using UnityEngine.UI;
using UnityEngine;
using Modules;

namespace UI.Controllers
{
    [RequireComponent(typeof(Button))]
    public sealed class HapticController : MonoBehaviour
    {
        [SerializeField] private HapticTypes _hapticType;
        [SerializeField] private Button _button;

        private void OnClick()
        {
            if (!_button.interactable)
                return;

            HapticProvider.Instance.Haptic(_hapticType);
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