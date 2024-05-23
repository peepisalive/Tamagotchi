using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class NavigationPanelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleLabel;

        public void SetText(string text)
        {
            _titleLabel.text = text;
        }
    }
}