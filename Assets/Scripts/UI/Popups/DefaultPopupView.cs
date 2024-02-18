using UnityEngine;
using System;
using TMPro;

namespace UI.Popups
{
    public sealed class DefaultPopupView : PopupView<DefaultPopup>
    {
        [Header("Labels")]
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _content;

        public override void Setup(DefaultPopup settings)
        {
            base.Setup(settings);

            SetTitle(settings.Title);
            SetContent(settings.Content);
        }

        public override void Show()
        {
            base.Show();
            DoShow();
        }

        public override void Hide(Action onHideCallback = null)
        {
            base.Hide(onHideCallback);
            DoHide(onHideCallback);
        }

        private void SetTitle(string text)
        {
            _title.text = text;
        }

        private void SetContent(string text)
        {
            _content.text = text;
        }
    }
}