using System.Collections.Generic;
using UI.Controller;
using UI.Settings;
using UnityEngine;
using System.Linq;
using Settings;
using System;
using TMPro;

namespace UI.Popups
{
    public sealed class DefaultPopupView : PopupView<DefaultPopup>
    {
        public List<DropdownController> Dropdowns { get; private set; }

        [Header("Labels")]
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _content;

        [Header("Other")]
        [SerializeField] private RectTransform _infoParent;

        public override void Setup(DefaultPopup settings)
        {
            base.Setup(settings);

            SetTitle(settings.Title);
            SetContent(settings.Content);
            SetDropdown(settings.DropdownSettings);
        }

        public override void Show()
        {
            base.Show();
            DoShow();
        }

        public override void Hide(Action onHideCallback = null)
        {
            base.Hide(onHideCallback);
            DoHide(onHideCallback);
        }

        private void SetDropdown(List<DropdownSettings> dropdownsSettings)
        {
            if (dropdownsSettings == null || !dropdownsSettings.Any())
                return;

            Dropdowns = new List<DropdownController>();

            var prefabSet = SettingsProvider.Get<PrefabsSet>();
            var dropdownPrefab = prefabSet.Dropdown;

            _infoParent.gameObject.SetActive(true);

            dropdownsSettings.ForEach(settings =>
            {
                var dropdown = Instantiate(dropdownPrefab, _infoParent);

                dropdown.Setup(settings);
                Dropdowns.Add(dropdown);
            });
        }

        private void SetTitle(string text)
        {
            _title.text = text;
        }

        private void SetContent(string text)
        {
            _content.gameObject.SetActive(true);
            _content.text = text;
        }
    }
}