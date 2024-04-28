using UnityEngine.UI;
using UnityEngine;

namespace UI.View
{
    public class ColorPickerView : MonoBehaviour
    {
        [SerializeField] private RawImage _hueImage;
        [SerializeField] private RawImage _svImage;

        public void SetHueTexture(Texture2D texture)
        {
            _hueImage.texture = texture;
        }

        public void SetSVTexture(Texture2D texture)
        {
            _svImage.texture = texture;
        }
    }
}