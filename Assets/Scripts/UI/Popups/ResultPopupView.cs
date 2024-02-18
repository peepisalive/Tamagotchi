using UnityEngine;
using System;
using TMPro;

namespace UI.Popups
{
    public sealed class ResultPopupView : PopupView<ResultPopup>
    {
        [Header("Labels")]
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _content;

        public override void Setup(ResultPopup settings)
        {
            base.Setup(settings);
            DoResultSetup();

            SetTitle(settings.Title);
            SetContent(settings.Content);
        }

        public override void Show()
        {
            base.Show();
            DoResultShow();
        }

        public override void Hide(Action onHideCallback = null)
        {
            Destroy(gameObject);
            onHideCallback?.Invoke();
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