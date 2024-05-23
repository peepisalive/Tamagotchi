using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class TextButtonView : ButtonView
    {
        [SerializeField] private TMP_Text _titleLabel;

        public void SetText(string text)
        {
            if (_titleLabel == null || text == null)
                return;

            _titleLabel.text = text;
        }
    }
}
