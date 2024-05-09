using System.Collections.Generic;
using UI.Controller;
using System.Linq;
using UI.Settings;
using UnityEngine;
using Settings;
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

        private void Setup()
        {
            _settings = SettingsProvider.Get<AccessoriesSettings>();

            var selectItems = (List<SelectItem<Accessory>>)default;
            var accessories = new List<Accessory>();

            FindObjectOfType<PetAppearance>().AccessoriesAppearances.ForEach(appearance =>
            {
                var accessory = _settings.GetAccessory(appearance.Type);

                accessory.SetModel(appearance.gameObject);
                accessories.Add(accessory);
            });

            accessories.Insert(0, new Accessory(AccessoryType.None, AccessType.Free, true, true));

            selectItems = accessories.Select(accessory =>
            {
                return new SelectItem<Accessory>(accessory, _settings.Localization.GetAccessoryName(accessory.Type));
            }).ToList();

            _currentAccessoryIndex = 0; // to do: del this
            _currentAccessory = selectItems[_currentAccessoryIndex].Item;
            _selectedAccessory = selectItems[_currentAccessoryIndex].Item;

            _selectPanel.Setup(selectItems.Cast<SelectItem>().ToList(), 0);
        }

        private void OnSelectItemChanged(SelectItem item, int index)
        {
            _selectedAccessory.Model?.SetActive(false);

            _selectedAccessory = ((SelectItem<Accessory>)item).Item;

            _currentAccessory.Model?.SetActive(false);
            _selectedAccessory.Model?.SetActive(true);

            _confirmButton.SetState(_currentAccessoryIndex != index);

            Debug.Log($"Current index: {index}");
        }

        private void OnConfirmButtonClick()
        {
            if (_selectPanel.CurrentState)
            {
                _currentAccessory = _selectPanel.GetCurrentSelectItem<Accessory>().Item;
                _currentAccessoryIndex = _selectPanel.CurrentItemIndex;

                _confirmButton.SetState(false);

                Debug.Log($"Current accessory: {_currentAccessory.Type}");
            }
            else
            {
                // to do: apply & save color

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
        }

        private void Start()
        {
            Setup();

            _selectPanel.OnValueChangeEvent += OnSelectItemChanged;
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
        }
    }
}