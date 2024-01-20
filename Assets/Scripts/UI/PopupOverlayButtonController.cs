using UnityEngine.UI;
using UnityEngine;
using Components;
using Modules;
using Events;

namespace UI.Controller
{
    public sealed class PopupOverlayButtonController : MonoBehaviour
    {
        [SerializeField] private RectTransform _root;
        [SerializeField] private Button _button;

        private void UpdateState(ShowPopupEvent e)
        {
            _root.gameObject.SetActive(true);

            if (e.IgnoreOverlayButton)
                return;

            _button.onClick.AddListener(SendHidePopupEvent);
        }

        private void UpdateState(HidePopupEvent e)
        {
            _root.gameObject.SetActive(false);
            _button.onClick?.RemoveListener(SendHidePopupEvent);
        }

        private void SendHidePopupEvent()
        {
            Tamagotchi.Application.Model.Send(new HidePopup());
        }

        private void Start()
        {
            EventSystem.Subscribe<ShowPopupEvent>(UpdateState);
            EventSystem.Subscribe<HidePopupEvent>(UpdateState);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<ShowPopupEvent>(UpdateState);
            EventSystem.Unsubscribe<HidePopupEvent>(UpdateState);
        }
    }
}