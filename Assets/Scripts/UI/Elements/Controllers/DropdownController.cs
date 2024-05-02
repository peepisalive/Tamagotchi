using UnityEngine.EventSystems;
using UnityEngine;
using UI.Settings;
using System.Linq;
using UI.View;
using TMPro;

namespace UI.Controller
{
    [RequireComponent(typeof(DropdownView))]
    public sealed class DropdownController : TMP_Dropdown
    {
        public int CurrentKey { get; private set; }

        private DropdownSettings _dropdownSettings;
        private DropdownView _dropdownView;
        private GameObject _contentParent;

        public T GetCurrentValue<T>()
        {
            return ((DropdownContent<T>)_dropdownSettings.DropdownContent[CurrentKey]).Value;
        }

        public void Setup(DropdownSettings dropdownSettings)
        {
            _dropdownSettings = dropdownSettings;
            _dropdownView.SetTitle(dropdownSettings.Title);

            AddOptions(dropdownSettings.DropdownContent.Select(content => content.Title).ToList());
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
        }

        protected override GameObject CreateBlocker(Canvas rootCanvas)
        {
            var obj = base.CreateBlocker(rootCanvas);
            SetContentParentState(true);

            return obj;
        }

        protected override void DestroyBlocker(GameObject gameObject)
        {
            base.DestroyBlocker(gameObject);
            SetContentParentState(false);
        }

        protected override void Awake()
        {
            base.Awake();

            _contentParent = GameObject.FindGameObjectWithTag("DropdownContentParent");
            _dropdownView = GetComponent<DropdownView>();

            onValueChanged.AddListener(OnValueChanged);
            SetContentParentState(false);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(int key)
        {
            CurrentKey = key;
        }

        private void SetContentParentState(bool state)
        {
            if (_contentParent == null)
                return;

            _contentParent.gameObject.SetActive(state);
        }
    }
}