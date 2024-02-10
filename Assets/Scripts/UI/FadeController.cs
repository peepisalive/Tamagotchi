using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Modules;
using System;
using Events;
using Core;

namespace UI.Controller
{
    public sealed class FadeController : MonoBehaviourSingleton<FadeController>
    {
        [SerializeField] private Image _image;
        private const float _fadeDuration = 1.1f;

        public void FadeOff(float fadeDuration = _fadeDuration, Action onFadeOffAction = null)
        {
            _image.DOFade(0f, fadeDuration)
                .SetLink(gameObject)
                .OnKill(() =>
                {
                    _image.enabled = false;
                    onFadeOffAction?.Invoke();
                });
        }

        public void FadeOn(float fadeDuration = _fadeDuration, Action onFadeOnAction = null)
        {
            _image.enabled = true;
            _image.DOFade(1f, fadeDuration)
                .SetLink(gameObject)
                .OnKill(() =>
                {
                    onFadeOnAction?.Invoke();
                });
        }

        private void OnScreenWithFadeOffShowedEvent(ScreenReplacedEvent data)
        {
            if (!data.FadeOffRequired)
                return;

            FadeOff(4f);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            EventSystem.Subscribe<ScreenReplacedEvent>(OnScreenWithFadeOffShowedEvent);
        }

        private void OnDestroy()
        {
            EventSystem.Unsubscribe<ScreenReplacedEvent>(OnScreenWithFadeOffShowedEvent);
        }
    }
}