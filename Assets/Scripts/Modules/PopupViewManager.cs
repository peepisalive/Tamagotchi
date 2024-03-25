using Object = UnityEngine.Object;
using Events.Popups;
using UnityEngine;
using System.Linq;
using UI.Popups;
using Settings;
using System;

namespace Modules
{
    public sealed class PopupViewManager
    {
        private PopupViewBase _currentPopup;
        private Transform _popupParent;
        private PrefabsSet _prefabsSet;

        public PopupViewManager()
        {
            _prefabsSet = SettingsProvider.Get<PrefabsSet>();
        }

        public void ShowPopup<T>(T settings) where T : Popup
        {
            EventSystem.Send(new OnShowPopupEvent
            {
                IgnoreOverlayButton = settings.IgnoreOverlayButton
            });

            if (_currentPopup != null)
            {
                HideCurrentPopup(() =>
                {
                    _currentPopup = null;
                    ShowPopup(settings);
                });

                return;
            }

            if (_popupParent == null)
                _popupParent = GameObject.FindGameObjectWithTag("PopupParent").transform;

            var popupPrefab = _prefabsSet.Popups.First(x => x.GetComponent<PopupView<T>>() != null)
                .GetComponent<PopupView<T>>();
            var instance = Object.Instantiate(popupPrefab, _popupParent, false);

            instance.Setup(settings);
            instance.Show();

            _currentPopup = instance;
        }

        public void HideCurrentPopup(Action onHideCallback = null)
        {
            if (_currentPopup == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Try close current Popup[null]");
#endif
                return;
            }

            if (onHideCallback == null)
                EventSystem.Send(new OnHidePopupEvent());

            _currentPopup.Hide(() =>
            {
                if (onHideCallback == null)
                    _currentPopup = null;

                onHideCallback?.Invoke();
            });
        }
    }
}
