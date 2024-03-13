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
        [SerializeField] private List<RectTransform> _parentBlocks; 

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

        protected void DoResultSetup()
        {
            foreach (var parentBlock in _parentBlocks)
            {
                parentBlock.localScale = Vector3.zero;
            }
        }

        protected void DoResultShow()
        {
            var sequence = DOTween.Sequence()
                .SetLink(gameObject);

            foreach (var parentBlock in _parentBlocks)
            {
                sequence.Append(parentBlock.DOScale(1f, 0.15f).SetEase(Ease.OutBack));
                sequence.AppendInterval(0.02f);
            }
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
                var setting = buttonSettings[0];

                if (setting.ActionWithInstance != null)
                    setting.PopupInstance = this;

                if (setting is TextButtonSettings textButtonSettings)
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
                        if (setting.ActionWithInstance != null)
                            setting.PopupInstance = this;

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
