using UnityEngine;
using TMPro;

namespace UI.View
{
    public sealed class MainScreenView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _petNameLabel;
        
        public void SetPetName(string text)
        {
            _petNameLabel.text = text;
        }
    }
}