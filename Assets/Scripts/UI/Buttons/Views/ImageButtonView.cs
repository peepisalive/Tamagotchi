using UnityEngine.UI;
using UnityEngine;

namespace UI.View
{
    public sealed class ImageButtonView : ButtonView
    {
        [Header("View")]
        [SerializeField] private Image _image;
    
        public void SetIcon(Sprite icon)
        {
            if (_image == null || icon == null)
                return;

            _image.sprite = icon;
        }
    }
}