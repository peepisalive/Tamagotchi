using UnityEngine;
using TMPro;

namespace UI.View
{
    public class ButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceLabel;

        public void SetPrice(string text)
        {
            _priceLabel.text = text;
        }
    }
}