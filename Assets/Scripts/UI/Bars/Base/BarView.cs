using UnityEngine.UI;
using UnityEngine;

namespace UI.View
{
    public sealed class BarView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private Image _iconImage;

        public void SetFillValue(float value, float previousValue = 0f)
        {
            _fillImage.fillAmount = value;
        }

        public void SetIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
        }
    }
}