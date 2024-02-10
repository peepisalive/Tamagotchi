using UnityEngine;
using System;

namespace UI.Popups
{
    public class PopupViewBase : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide(Action callback = null) { }
    }
}
