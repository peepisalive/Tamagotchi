using UnityEngine.UI;
using Events.Popups;
using UnityEngine;
using Modules;

namespace UI.Controller
{
    public sealed class PopupOverlayButtonController : MonoBehaviour
    {
        [SerializeField] private RectTransform _root;
        [SerializeField] private Button _button;

        private void UpdateState(OnShowPopupEvent e)
        {
            if (_root.gameObject.activeInHierarchy)
                return;

            _root.gameObject.SetActive(true);

            if (e.IgnoreOverlayButton)
                return;

            _button.onClick.AddListener(SendHidePopupEvent);
        }

        private void UpdateState(OnHidePopupEvent e)
        {
            _root.gameObject.SetActive(false);
            _button.onClick?.RemoveListener(SendHidePopupEvent);
        }

        private void SendHidePopupEvent()
        {
            EventSystem.Send(new HidePopupEvent());
        }

        private void Start()
        {
            EventSystem.Subscribe<OnShowPopupEvent>(UpdateState);
            EventSystem.Subscribe<OnHidePopupEvent>(UpdateState);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<OnShowPopupEvent>(UpdateState);
            EventSystem.Unsubscribe<OnHidePopupEvent>(UpdateState);
        }
    }
}