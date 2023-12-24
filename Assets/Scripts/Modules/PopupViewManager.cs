using Object = UnityEngine.Object;
using UnityEngine;
using System.Linq;
using UI.Popups;
using Settings;

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
            if (_currentPopup != null)
                return;

            if (_popupParent == null)
                _popupParent = GameObject.FindGameObjectWithTag("PopupParent").transform;

            var popupPrefab = _prefabsSet.Popups.First(x => x.GetComponent<PopupView<T>>() != null)
                .GetComponent<PopupView<T>>();
            var instance = Object.Instantiate(popupPrefab, _popupParent, false);

            instance.Setup(settings);
            instance.Show();

            _currentPopup = instance;
        }

        public void HideCurrentPopup()
        {
            if (_currentPopup == null)
            {
#if DEBUG
                Debug.LogError("Try close current Popup[null]");
#endif
                return;
            }

            _currentPopup.Hide();
        }
    }
}
