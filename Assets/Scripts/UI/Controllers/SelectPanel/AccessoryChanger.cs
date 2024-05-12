using Application = Tamagotchi.Application;
using System.Collections.Generic;
using UI.Controller;
using System.Linq;
using UI.Settings;
using UnityEngine;
using Settings;
using Modules;
using Events;
using Core;

namespace UI
{
    public sealed class AccessoryChanger : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private SelectPanelController _selectPanel;
        [SerializeField] private ColorPickerController _colorPicker;

        [Header("Buttons")]
        [SerializeField] private ImageButtonController _colorChangeButton;
        [SerializeField] private TextButtonController _confirmButton;

        private AccessoriesSettings _settings;

        private Accessory _currentAccessory;
        private Accessory _selectedAccessory;
        private int _currentAccessoryIndex;

        private List<AccessoryAppearance> _accessoriesAppearances;
        private Pet _pet;

        private void Setup()
        {
            _settings = SettingsProvider.Get<AccessoriesSettings>();
            _pet = Application.Model.GetCurrentPet();
            _accessoriesAppearances = FindObjectOfType<PetAppearance>().AccessoriesAppearances;

            var accessories = _pet.Accessories;

            _accessoriesAppearances.ForEach(appearance =>
            {
                var accessory = accessories.First(a => a.Type == appearance.Type);

                accessory.SetModel(appearance.gameObject);

                if (accessory.Color != default)
                    appearance.SetColor(accessory.Color);
            });

            var currentAccessory = accessories.First(a => a.IsCurrent);
            var selectItems = accessories.Select(accessory =>
            {
                return new SelectItem<Accessory>(accessory, _settings.Localization.GetAccessoryName(accessory.Type));
            }).ToList();

            _currentAccessoryIndex = accessories.IndexOf(accessories.First(a => a.Type == currentAccessory.Type));
            _currentAccessory = selectItems[_currentAccessoryIndex].Item;
            _selectedAccessory = selectItems[_currentAccessoryIndex].Item;

            _colorChangeButton.SetState(_selectedAccessory.Model != null);
            _selectPanel.Setup(selectItems.Cast<SelectItem>().ToList(), _currentAccessoryIndex);
        }

        private void OnSelectItemChanged(SelectItem item, int index)
        {
            _selectedAccessory.Model?.SetActive(false);

            _selectedAccessory = ((SelectItem<Accessory>)item).Item;

            _currentAccessory.Model?.SetActive(false);
            _selectedAccessory.Model?.SetActive(true);

            _colorChangeButton.SetState(_selectedAccessory.Model != null);
            _confirmButton.SetState(_currentAccessoryIndex != index);

            _confirmButton.SetAdsSignState(_selectedAccessory.AccessType == AccessType.Ads && !_selectedAccessory.IsUnlocked);
            _confirmButton.SetMoneySignState(_selectedAccessory.AccessType == AccessType.Money && !_selectedAccessory.IsUnlocked);

            Debug.Log($"Current index: {index}");
        }

        private void OnItemColorChanged(Color color)
        {
            _pet.Accessories.First(a => a.Type == _selectedAccessory.Type).SetColor(color);
            _accessoriesAppearances.First(a => a.Type == _selectedAccessory.Type).SetColor(color);
        }

        private void HandleUnlockAccessoryEvent(UnlockAccessoryEvent e)
        {
            UnlockAccessory();
            SwitchCurrentAccessory();
        }

        #region Button click handlers
        private void OnConfirmButtonClick()
        {
            if (_selectPanel.CurrentState)
            {
                if (_selectedAccessory.IsUnlocked)
                {
                    SwitchCurrentAccessory();
                }
                else
                {
                    if (_selectedAccessory.TryPurchase())
                    {
                        UnlockAccessory();
                        SwitchCurrentAccessory();
                    }
                }

                Debug.Log($"Current accessory: {_currentAccessory.Type}");
            }
            else
            {
                EventSystem.Send(new PetCameraRotateStateEvent(true));
                _colorPicker.OnColorChangeEvent -= OnItemColorChanged;

                _colorPicker.SetState(false);
                _selectPanel.SetState(true);

                _confirmButton.SetState(_currentAccessoryIndex != _selectPanel.CurrentItemIndex);
            }
        }

        private void OnColorButtonClick()
        {
            if (_colorPicker.CurrentState)
                return;

            _selectPanel.SetState(false);

            _confirmButton.SetState(true);
            _colorPicker.SetState(true);

            _colorPicker.OnColorChangeEvent += OnItemColorChanged;
            EventSystem.Send(new PetCameraRotateStateEvent(false));
        }
        #endregion

        private void UnlockAccessory()
        {
            _selectedAccessory.SetUnlockState(true);
        }

        private void SwitchCurrentAccessory()
        {
            _currentAccessory.SetCurrentState(false);
            _selectedAccessory.SetCurrentState(true);

            _currentAccessory = _selectedAccessory;
            _currentAccessoryIndex = _selectPanel.CurrentItemIndex;

            _confirmButton.SetState(false);
            _confirmButton.SetAdsSignState(false);
        }

        private void Start()
        {
            Setup();

            _selectPanel.OnValueChangeEvent += OnSelectItemChanged;
            EventSystem.Subscribe<UnlockAccessoryEvent>(HandleUnlockAccessoryEvent);

            _confirmButton.Setup(new TextButtonSettings
            {
                Title = _settings.Localization.SaveChangesTitle,
                Action = OnConfirmButtonClick
            });
            _colorChangeButton.Setup(new ImageButtonSettings
            {
                Action = OnColorButtonClick
            });
        }

        private void OnDestroy()
        {
            _selectPanel.OnValueChangeEvent -= OnSelectItemChanged;
            EventSystem.Unsubscribe<UnlockAccessoryEvent>(HandleUnlockAccessoryEvent);
        }
    }
}