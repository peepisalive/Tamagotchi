using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class TextButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleLabel;
        [SerializeField] private Image _buttonImage;

        public void SetText(string text)
        {
            if (_titleLabel == null)
                return;

            _titleLabel.text = text;
        }

        public void SetButtonColor(Color? color)
        {
            if (_buttonImage == null || color == null)
                return;

            _buttonImage.color = (Color)color;
        }
    }
}
