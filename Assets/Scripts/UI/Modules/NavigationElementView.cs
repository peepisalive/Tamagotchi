using UnityEngine;

namespace UI.Modules.Navigation
{
    public sealed class NavigationElementView : MonoBehaviour
    {
        [SerializeField] private RectTransform _notifyIcon;

        public void SetNotifyIconState(bool state)
        {
            _notifyIcon.gameObject.SetActive(state);
        }
    }
}