using UnityEngine.UI;
using UnityEngine;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(ColorPickerView))]
    public class ColorPickerController : MonoBehaviour
    {
        [SerializeField] private ColorPickerView _view;
        [SerializeField] private Slider _slider;

        private Texture2D _hueTexture;
        private Texture2D _svTexture;

        private float _currentValue;
        private float _currentHue;
        private float _currentSV;

        private void UpdateSVTexture(float currentSliderValue)
        {
            _currentHue = currentSliderValue;

            CreateSVTexture();
        }

        private void InitializeHueTexture()
        {
            _hueTexture = new Texture2D(1, 16);
            _hueTexture.name = nameof(_hueTexture);
            _hueTexture.wrapMode = TextureWrapMode.Clamp;

            for (int i = 0; i < _hueTexture.height; ++i)
            {
                _hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / _hueTexture.height, 1f, 0.9f));
            }

            _hueTexture.Apply();
            _view.SetHueTexture(_hueTexture);

            _currentHue = 0f;
        }

        private void InitializeSVTexture()
        {
            _svTexture = new Texture2D(16, 16);
            _svTexture.name = nameof(_svTexture);
            _svTexture.wrapMode = TextureWrapMode.Clamp;

            CreateSVTexture();
            _view.SetSVTexture(_svTexture);

            _currentSV = 0f;
            _currentValue = 0f;
        }

        private void CreateSVTexture()
        {
            for (int y = 0; y < _svTexture.height; ++y)
            {
                for (int x = 0; x < _svTexture.width; ++x)
                {
                    _svTexture.SetPixel(x, y, Color.HSVToRGB(_currentHue, (float)x / _svTexture.width, (float)y / _svTexture.height));
                }
            }

            _svTexture.Apply();
        }

        private void Awake()
        {
            InitializeHueTexture();
            InitializeSVTexture();

            _slider.onValueChanged.AddListener(UpdateSVTexture);
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(UpdateSVTexture);

            Destroy(_svTexture);
            Destroy(_hueTexture);
        }
    }
}