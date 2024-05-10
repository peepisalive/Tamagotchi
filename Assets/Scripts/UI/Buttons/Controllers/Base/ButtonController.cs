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
        [SerializeField] private RectTransform _adsSignParent;

        private Tween _tween;
        private Tween _adsParentTween;

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
                if (_adsSignParent.gameObject.activeInHierarchy)
                    return;

                _adsParentTween?.Kill();

                _adsSignParent.localScale = Vector3.zero;
                _adsParentTween = _adsSignParent.DOScale(Vector3.one, 0.075f)
                    .SetLink(gameObject);
            }

            _adsSignParent.gameObject.SetActive(state);
        }

        private void OnDestroy()
        {
            _button?.onClick?.RemoveAllListeners();
        }
    }
}