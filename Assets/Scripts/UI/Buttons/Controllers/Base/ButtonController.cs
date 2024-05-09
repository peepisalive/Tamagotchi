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

        private Tween _tween;

        public void SetState(bool state)
        {
            if (state)
            {
                if (CurrentState)
                    return;

                gameObject.SetActive(true);

                _tween?.Kill();

                _button.transform.localScale = Vector3.zero;
                _tween = _button.transform.DOScale(Vector3.one, 0.075f)
                    .SetLink(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public virtual void Setup(ButtonSettings settings)
        {
            _button?.onClick.AddListener(() => settings.Action?.Invoke());
            _button?.onClick.AddListener(() => settings.ActionWithInstance?.Invoke(settings.PopupInstance));
        }

        private void OnDestroy()
        {
            _button?.onClick?.RemoveAllListeners();
        }
    }
}