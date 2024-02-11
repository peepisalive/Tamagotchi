using Application = Tamagotchi.Application;
using System.Collections.Generic;
using UI.Controller;
using UnityEngine;
using DG.Tweening;
using UI.Settings;
using System.Linq;
using Settings;
using System;

namespace UI.Popups
{
    public class PopupView<T> : PopupViewBase where T : Popup
    {
        [Header("Base")]
        [SerializeField] private RectTransform _popupRect;

        [Header("Blocks")]
        [SerializeField] private RectTransform _topParent;
        [SerializeField] private RectTransform _middleParent;
        [SerializeField] private RectTransform _bottomParent;

        [Header("Button parents")]
        [SerializeField] private RectTransform _someButtonParent;
        [SerializeField] private RectTransform _oneButtonParent;

        private readonly float _durationTween = 0.25f;

        public virtual void Setup(T settings)
        {
            InitializeButtons(settings.ButtonSettings);
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide(Action onHideCallback = null)
        {
            base.Hide();
        }

        protected void DoShow()
        {
            var startOffset = Vector3.down.normalized * Application.MainCanvas.sizeDelta.y;
            var targetPosition = _popupRect.localPosition;

            if (Mathf.Abs(startOffset.sqrMagnitude) - Mathf.Abs(Vector2.zero.sqrMagnitude) <= Mathf.Epsilon)
                return;

            _popupRect.localPosition += startOffset;
            _popupRect.DOAnchorPos(targetPosition, _durationTween)
                .SetEase(Ease.InOutCubic);
        }

        protected void DoHide(Action onHideCallback = null)
        {
            var targetPosition = Vector3.down.normalized * Application.MainCanvas.sizeDelta.y;

            if (Mathf.Abs(targetPosition.sqrMagnitude) - Mathf.Abs(Vector2.zero.sqrMagnitude) <= Mathf.Epsilon)
                return;

            _popupRect.DOAnchorPos(targetPosition, _durationTween)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() =>
                {
                    onHideCallback?.Invoke();
                    Destroy(gameObject);
                });
        }

        protected void DoResultShow()
        {
            var sequence = DOTween.Sequence()
                .SetLink(gameObject);

            sequence.Append(_topParent.DOScale(0f, 0f));
            sequence.Append(_middleParent.DOScale(0f, 0f));
            sequence.Append(_bottomParent.DOScale(0f, 0f));

            sequence.Append(_topParent.DOScale(1f, 0.1f).SetEase(Ease.OutBack));
            sequence.AppendInterval(0.02f);
            sequence.Append(_middleParent.DOScale(1f, 0.1f).SetEase(Ease.OutBack));
            sequence.AppendInterval(0.02f);
            sequence.Append(_bottomParent.DOScale(1f, 0.1f).SetEase(Ease.OutBack));
        }

        private void InitializeButtons<B>(List<B> buttonSettings) where B : ButtonSettings
        {
            if (buttonSettings == null || buttonSettings.Count == 0)
                return;

            var prefabsSet = SettingsProvider.Get<PrefabsSet>();

            _oneButtonParent.gameObject.SetActive(buttonSettings.Count == 1);
            _someButtonParent.gameObject.SetActive(buttonSettings.Count != 1);

            if (buttonSettings.Count == 1)
            {
                var settings = buttonSettings[0];

                if (settings is TextButtonSettings textButtonSettings)
                {
                    var prefab = prefabsSet.Buttons.First(x => x.GetComponent<TextButtonController>() != null)
                        .GetComponent<TextButtonController>();

                    Instantiate(prefab, _oneButtonParent, false)
                        .Setup(textButtonSettings);
                }
            }
            else
            {
                foreach (var setting in buttonSettings)
                {
                    if (setting is TextButtonSettings textButtonSettings)
                    {
                        var prefab = prefabsSet.Buttons.First(x => x.GetComponent<TextButtonController>() != null)
                            .GetComponent<TextButtonController>();

                        Instantiate(prefab, _someButtonParent, false)
                            .Setup(textButtonSettings);
                    }
                }
            }
        }
    }
}
