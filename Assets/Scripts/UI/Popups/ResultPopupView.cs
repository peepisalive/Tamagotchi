using UnityEngine;
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

        private void SetTitle(string text)
        {
            _title.text = text;
        }
    }
}