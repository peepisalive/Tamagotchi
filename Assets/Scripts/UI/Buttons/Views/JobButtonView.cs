using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI.View
{
    public class JobButtonView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        [Header("Labels")]
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetTitle(string text)
        {
            _title.text = text;
        }

        public void SetDescription(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            _description.gameObject.SetActive(true);
            _description.text = text;
        }
    }
}