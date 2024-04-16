using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class JobButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;

        public void SetTitle(string text)
        {
            _title.text = text;
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}