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
            _mainIcon.sprite = icon;
        }

        public void SetTransitionIcon(Sprite icon)
        {
            _transitionIcon.sprite = icon;
        }

        public void SetTitle(string text)
        {
            _titleLabel.text = text;
        }

        public void SetContent(string text)
        {
            _contentLabel.text = text;
        }
    }
}