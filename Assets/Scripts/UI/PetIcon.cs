using Application = Tamagotchi.Application;
using UnityEngine.UI;
using System.Linq;
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
        private PetCamera _petCamera;
        private Pet _pet;

        private static float _currentOffsetX = 100f;

        private void Setup()
        {
            _petContainer = GameObject.FindGameObjectWithTag("PetContainer").transform;
            _pet = Application.Model.GetCurrentPet();

            ReleaseResources();
            InitializeAppearance();
            InitializeCamera();
            InitializeRenderTexture();
        }

        private void InitializeRenderTexture()
        {
            if (_petCamera.Camera.targetTexture != null)
                return;
            
            _petCamera.Camera.targetTexture = new RenderTexture(new RenderTextureDescriptor(1024, 1024, RenderTextureFormat.Default));
            _rawImage.texture = _petCamera.Camera.targetTexture;
        }

        private void InitializeCamera()
        {
            _petCamera = Instantiate(SettingsProvider.Get<PrefabsSet>().PetCamera, _petContainer);
            _petCamera.SetTarget(_petAppearance.transform);
        }

        private void InitializeAppearance()
        {
            var settings = SettingsProvider.Get<PetAppearanceSettings>();
            var petAppearancePrefab = settings.GetAppearance(_pet.Type);

            _petAppearance = Instantiate(petAppearancePrefab, _petContainer);
            _currentOffsetX = -_currentOffsetX;

            _petAppearance.transform.localPosition = new Vector3(_currentOffsetX, 0f, 0f);

            if (_pet.Accessories.Any(a => a.Type != AccessoryType.None && a.IsCurrent))
            {
                var currentAccessory = _pet.Accessories.First(a => a.Type != AccessoryType.None && a.IsCurrent);
                var accessoryAppearance = _petAppearance.AccessoriesAppearances.First(aa => aa.Type == currentAccessory.Type);

                accessoryAppearance.SetColor(currentAccessory.Color);
                accessoryAppearance.gameObject.SetActive(true);
            }
        }

        private void ReleaseResources()
        {
            if (_petCamera != null)
            {
                _petCamera.Camera.targetTexture.Release();
                Destroy(_petCamera.gameObject);
            }

            if (_rawImage != null && _rawImage.texture is RenderTexture renderTexture)
            {
                renderTexture.Release();
            }

            if (_petAppearance != null)
                Destroy(_petAppearance.gameObject);
        }

        private void Awake()
        {
            Setup();
        }

        private void OnDestroy()
        {
            ReleaseResources();
        }
    }
}