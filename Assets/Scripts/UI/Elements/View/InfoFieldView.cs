using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class InfoFieldView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleLabel;
        [SerializeField] private Image _icon;

        public void SetTitle(string text)
        {
            _titleLabel.text = text;
        }

        public void SetIcon(Sprite icon, bool state)
        {
            _icon.gameObject.SetActive(state);

            if (icon == null)
                return;

            _icon.sprite = icon;
        }
    }
}