using UnityEngine;

namespace UI.Popups
{
    public class PopupViewBase : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide() { }
    }
}
