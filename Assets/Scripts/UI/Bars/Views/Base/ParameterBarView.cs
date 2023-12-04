using UnityEngine.UI;
using UnityEngine;

namespace UI.View
{
    public abstract class ParameterBarView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;

        public abstract void SetValue(float value);
        public abstract void SetText(string text);
        public virtual void SetIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
        }
    }
}