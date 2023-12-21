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

        public void SetIcon()
        {
            if (_mainIcon == null)
                return;
        }

        private void SetTransitionIcon()
        {
            if (_transitionIcon == null)
                return;
        }

        private void SetTitle()
        {
            if (_titleLabel == null)
                return;
        }

        private void SetContent()
        {
            if (_contentLabel == null)
                return;
        }
    }
}