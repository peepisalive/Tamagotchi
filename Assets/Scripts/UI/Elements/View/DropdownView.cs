using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class DropdownView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleLabel;

        public void SetTitle(string text)
        {
            _titleLabel.text = text;
        }
    }
}