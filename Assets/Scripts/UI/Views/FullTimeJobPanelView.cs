using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class FullTimeJobPanelView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _timeLabel;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetTime(string text)
        {
            _timeLabel.text = text;
        }
    }
}