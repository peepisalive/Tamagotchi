using UnityEngine;
using UI.Settings;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;

namespace UI.Controller
{
    public sealed class DropdownController : TMP_Dropdown
    {
        public int CurrentKey { get; private set; } 

        private GameObject _contentParent;

        public void Setup(DropdownSettings dropdownSettings)
        {
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