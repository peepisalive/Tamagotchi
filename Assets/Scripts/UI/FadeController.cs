using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System;
using Core;

namespace UI.Controller
{
    public sealed class FadeController : MonoBehaviourSingleton<FadeController>
    {
        [SerializeField] private Image _image;
        private const float _fadeDuration = 1.1f;

        public void FadeOff(Action onFadeOffAction = null)
        {
            _image.DOFade(0f, _fadeDuration)
                .SetLink(gameObject)
                .OnKill(() =>
                {
                    _image.enabled = false;
                    onFadeOffAction?.Invoke();
                });
        }

        public void FadeOn(Action onFadeOnAction = null)
        {
            _image.enabled = true;
            _image.DOFade(1f, _fadeDuration)
                .SetLink(gameObject)
                .OnKill(() =>
                {
                    onFadeOnAction?.Invoke();
                });
        }

        private void Start()
        {
            FadeOff();
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}