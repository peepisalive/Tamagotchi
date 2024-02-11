using UnityEngine;
using System;
using TMPro;

namespace UI.Popups
{
    public sealed class ResultPopupView : PopupView<ResultPopup>
    {
        [Header("Labels")]
        [SerializeField] private TMP_Text _title;

        public override void Setup(ResultPopup settings)
        {
            base.Setup(settings);

            SetTitle(settings.Title);
        }

        public override void Show()
        {
            base.Show();
            DoResultShow();
        }

        public override void Hide(Action onHideCallback = null)
        {
            base.Hide(onHideCallback);
        }

        private void SetTitle(string text)
        {
            _title.text = text;
        }
    }
}