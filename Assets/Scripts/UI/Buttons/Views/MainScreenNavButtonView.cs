using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class MainScreenNavButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleLabel;

        public void SetTitle(string text)
        {
            if (_titleLabel == null)
                return;

            _titleLabel.text = text;
        }
    }
}