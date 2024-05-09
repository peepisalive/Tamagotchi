using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class SelectPanelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _itemLabel;
        
        public void SetItemText(string text)
        {
            _itemLabel.text = text;
        }
    }
}