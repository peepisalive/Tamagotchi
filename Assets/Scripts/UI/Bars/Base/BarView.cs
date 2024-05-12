using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Utils;

namespace UI.View
{
    public sealed class BarView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private Image _iconImage;

        private Tween _fillImageTween;
        private Tween _colorImageTween;

        private const float TWEEN_DURATION = 1f;

        public void SetFillValue(float value, bool invertColor = false)
        {
            var duration = Mathf.Approximately(_fillImage.fillAmount, value) ? 0f : TWEEN_DURATION;
            var color = ScreenUtils.GetBarColor(invertColor ? 1 - value : value);

            _fillImageTween?.Kill();
            _colorImageTween?.Kill();

            _fillImageTween = _fillImage.DOFillAmount(value, duration)
                .SetLink(gameObject);
            _colorImageTween = _fillImage.DOColor(color, duration)
                .SetLink(gameObject);
        }

        public void SetIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
        }
    }
}