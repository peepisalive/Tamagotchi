using UnityEngine;

namespace UI
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
