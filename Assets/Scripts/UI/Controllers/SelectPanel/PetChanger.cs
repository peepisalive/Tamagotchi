using Modules.Localization;
using UI.Settings;
using UnityEngine;

namespace UI.Controller
{
    public sealed class PetChanger : ItemChanger
    {
        [SerializeField] private TextButtonController _confirmButton;

        public override void Setup()
        {

        }

        protected override void Start()
        {
            base.Start();

            _confirmButton.Setup(new TextButtonSettings
            {
                Title = LocalizationProvider.GetText("save_changes/button"),
                Action = OnConfirmButtonClick
            });
        }

        protected override void OnSelectItemChanged(SelectItem item, int index)
        {
            
        }

        private void OnConfirmButtonClick()
        {

        }
    }
}