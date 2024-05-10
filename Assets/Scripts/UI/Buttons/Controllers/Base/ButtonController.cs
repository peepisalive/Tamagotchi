using UnityEngine.UI;
using UI.Settings;
using UnityEngine;
using DG.Tweening;

namespace UI.Controller
{
    [RequireComponent(typeof(Button))]
    public class ButtonController : MonoBehaviour, IStateSettable
    {
        public bool CurrentState => gameObject.activeInHierarchy;

        [Header("Base")]
        [SerializeField] private Button _button;

        [Header("Signs")]
        [SerializeField] private RectTransform _adsSign;
        [SerializeField] private RectTransform _moneySign;

        private Tween _tween;
        private Tween _adsSignTween;
        private Tween _moneySignTween;

        public virtual void Setup(ButtonSettings settings)
        {
            _button?.onClick.AddListener(() => settings.Action?.Invoke());
            _button?.onClick.AddListener(() => settings.ActionWithInstance?.Invoke(settings.PopupInstance));
        }

        public void SetState(bool state)
        {
            if (state)
            {
                if (CurrentState)
                    return;

                _tween?.Kill();

                _button.transform.localScale = Vector3.zero;
                _tween = _button.transform.DOScale(Vector3.one, 0.075f)
                    .SetLink(gameObject);
            }

            gameObject.SetActive(state);
        }

        public void SetAdsSignState(bool state)
        {
            if (state)
            {
                SetMoneySignState(false);

                if (_adsSign.gameObject.activeInHierarchy)
                    return;

                KillSignTweens();

                _adsSign.localScale = Vector3.zero;
                _adsSignTween = _adsSign.DOScale(Vector3.one, 0.075f)
                    .SetLink(gameObject);
            }

            _adsSign.gameObject.SetActive(state);
        }

        public void SetMoneySignState(bool state)
        {
            if (state)
            {
                SetAdsSignState(false);

                if (_moneySign.gameObject.activeInHierarchy)
                    return;

                KillSignTweens();

                _moneySign.localScale = Vector3.zero;
                _moneySignTween = _moneySign.DOScale(Vector3.one, 0.075f)
                    .SetLink(gameObject);
            }

            _moneySign.gameObject.SetActive(state);
        }

        private void KillSignTweens()
        {
            _adsSignTween?.Kill();
            _moneySignTween?.Kill();
        }

        private void OnDestroy()
        {
            _button?.onClick?.RemoveAllListeners();
        }
    }
}