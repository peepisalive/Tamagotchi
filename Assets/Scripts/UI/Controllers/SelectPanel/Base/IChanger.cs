using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Controller
{
    public abstract class ItemChanger : MonoBehaviour
    {
        protected int SelectItemIndex => _selectPanel.SelectItemIndex;
        protected bool SelectPanelState => _selectPanel.CurrentState;

        [Header("Base")]
        [SerializeField] private SelectPanelController _selectPanel;

        public abstract void Setup();

        public void SetupSelectPanel<T>(List<T> items, int currentIndex) where T : class
        {
            _selectPanel.Setup(items.Cast<SelectItem>().ToList(), currentIndex);
        }

        public void SetSelectPanelState(bool state)
        {
            _selectPanel.SetState(state);
        }

        protected abstract void OnSelectItemChanged(SelectItem item, int index);

        protected virtual void Start()
        {
            _selectPanel.OnValueChangeEvent += OnSelectItemChanged;
        }

        protected virtual void OnDestroy()
        {
            _selectPanel.OnValueChangeEvent -= OnSelectItemChanged;
        }
    }
}