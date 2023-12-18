using UnityEngine.UI;
using UnityEngine;
using Settings;
using Core;

namespace UI
{
    [RequireComponent(typeof(RawImage))]
    public sealed class PetIcon : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;

        private PetAppearance _petAppearance;
        private Transform _petContainer;
        private Camera _petCamera;
        private Pet _pet;

        public void Setup(Pet pet)
        {
            if (pet == null)
                return;
            
            _petContainer = GameObject.FindGameObjectWithTag("PetContainer").transform;
            _pet = pet;

            ReleaseResources();
            InitializeAppearance();
            InitializeCamera();
            InitializeRenderTexture();
        }

        // test
        private void Start()
        {
            Setup(new Pet(""));
        }
        // test

        private void InitializeRenderTexture()
        {
            if (_petCamera.targetTexture != null)
                return;

            _petCamera.targetTexture = new RenderTexture(new RenderTextureDescriptor(1024, 1024, RenderTextureFormat.Default));
            _rawImage.texture = _petCamera.targetTexture;
        }

        private void InitializeCamera()
        {
            _petCamera = Instantiate(SettingsProvider.Get<PrefabsSet>().PetCamera, _petContainer);
            _petCamera.transform.position = new Vector3(_petCamera.transform.position.x, _petCamera.transform.position.y, _petCamera.transform.position.z - 10);
        }

        private void InitializeAppearance()
        {
            _petAppearance = Instantiate(SettingsProvider.Get<PrefabsSet>().PetAppearance, _petContainer);
        }

        private void ReleaseResources()
        {
            if (_petCamera != null)
            {
                _petCamera.targetTexture.Release();
                Destroy(_petCamera.gameObject);
            }

            if (_rawImage != null && _rawImage.texture is RenderTexture renderTexture)
            {
                renderTexture.Release();
            }

            if (_petAppearance != null)
                Destroy(_petAppearance.gameObject);
        }

        private void OnDestroy()
        {
            ReleaseResources();
        }
    }
}