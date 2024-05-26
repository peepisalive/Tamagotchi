using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class InfoFieldView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        [Header("Labels")]
        [SerializeField] private TMP_Text _titleLabel;
        [SerializeField] private TMP_Text _contentLabel;

        public void SetTitle(string text)
        {
            _titleLabel.text = text;
        }

        public void SetContent(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            _contentLabel.gameObject.SetActive(true);
            _contentLabel.text = text;
        }

        public void SetIcon(Sprite icon, bool state)
        {
            _icon.gameObject.SetActive(state);

            if (icon == null || !state)
                return;

            _icon.sprite = icon;
        }
    }
}