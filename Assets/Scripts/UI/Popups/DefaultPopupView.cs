using UnityEngine;
using System;
using TMPro;

namespace UI.Popups
{
    public sealed class DefaultPopupView : PopupView<DefaultPopup>
    {
        [Header("Labels")]
        [SerializeField] private TMP_Text _title;

        public override void Setup(DefaultPopup settings)
        {
            base.Setup(settings);

            SetTitle(settings.Title);
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
    }
}