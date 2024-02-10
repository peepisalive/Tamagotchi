using UnityEngine.UI;
using UnityEngine;

namespace UI.View
{
    public sealed class ParameterBarView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private Image _iconImage;

        public void SetFillValue(float value, float previousValue)
        {
            _fillImage.fillAmount = value;
        }

        public void SetIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
        }
    }
}