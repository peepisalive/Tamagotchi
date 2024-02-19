using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace UI.View
{
    public sealed class BarView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private Image _iconImage;

        private Tween _tween;

        public void SetFillValue(float value)
        {
            _tween.Kill();
            _tween = _fillImage.DOFillAmount(value, 1f)
                .SetLink(gameObject);
        }

        public void SetIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
        }
    }
}