using Modules.Navigation;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class NavigationButtonView : MonoBehaviour
    {
        [Header("Images")]
        [SerializeField] private Image _mainIcon;
        [SerializeField] private Image _transitionIcon;

        [Header("Labels")]
        [SerializeField] private TMP_Text _titleLabel;
        [SerializeField] private TMP_Text _contentLabel;

        public void SetIcon(Sprite icon)
        {
            if (_mainIcon == null)
                return;

            _mainIcon.sprite = icon;
        }

        public void SetTransitionIcon(NavigationButtonState type)
        {
            if (_transitionIcon == null)
                return;
        }

        public void SetTitle(string text)
        {
            if (_titleLabel == null)
                return;

            _titleLabel.text = text;
        }

        public void SetContent(string text)
        {
            if (_contentLabel == null)
                return;

            _contentLabel.text = text;
        }
    }
}