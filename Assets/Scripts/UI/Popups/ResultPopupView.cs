using System.Collections.Generic;
using UI.Settings;
using UnityEngine;
using System.Linq;
using Settings;
using System;
using TMPro;

namespace UI.Popups
{
    public sealed class ResultPopupView : PopupView<ResultPopup>
    {
        [Header("Labels")]
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _content;

        [Header("Parents")]
        [SerializeField] private RectTransform _infoParent;
        [SerializeField] private RectTransform _parametersParent;

        public override void Setup(ResultPopup settings)
        {
            base.Setup(settings);
            DoResultSetup();

            SetTitle(settings.Title);
            SetContent(settings.Content);
            SetParameterBars(settings.InfoParameterSettings);
        }

        public override void Show()
        {
            base.Show();
            DoResultShow();
        }

        public override void Hide(Action onHideCallback = null)
        {
            Destroy(gameObject);
            onHideCallback?.Invoke();
        }

        private void SetTitle(string text)
        {
            if (text == null)
                return;

            _title.text = text;
        }

        private void SetContent(string text)
        {
            if (text == null)
                return;

            _content.text = text;
        }

        private void SetParameterBars(List<InfoParameterSettings> settings)
        {
            if (settings == null || !settings.Any())
                return;

            var prefab = SettingsProvider.Get<PrefabsSet>().InfoBar;
            var parametersSettings = SettingsProvider.Get<ParametersSettings>();

            _infoParent.gameObject.SetActive(true);
            _parametersParent.gameObject.SetActive(true);

            settings.ForEach(s =>
            {
                Instantiate(prefab, _parametersParent)
                    .Setup(s.Value, parametersSettings.GetIcon(s.Type));
            });
        }
    }
}