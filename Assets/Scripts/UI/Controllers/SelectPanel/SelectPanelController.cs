using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UI.View;
using System;

namespace UI.Controller
{
    [RequireComponent(typeof(SelectPanelView))]
    public sealed class SelectPanelController : MonoBehaviour, IStateSettable
    {
        public event Action<SelectItem, int> OnValueChangeEvent;

        public bool CurrentState => gameObject.activeInHierarchy;
        public int SelectItemIndex { get; private set; }

        [SerializeField] private SelectPanelView _view;

        [Header("Buttons")]
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        private List<SelectItem> _selectItems;

        public void Setup(List<SelectItem> selectItems, int currentItemIndex)
        {
            _selectItems = selectItems;
            SelectItemIndex = currentItemIndex;

            _view.SetItemText(_selectItems[SelectItemIndex].Title);
            SetButtonsStates();
        }

        public void SetState(bool state)
        {
            gameObject.SetActive(state);

            if (state)
            {
                transform.localScale = Vector3.zero;
                transform.DOScale(Vector3.one, 0.075f)
                    .SetLink(gameObject);
            }
        }

        public SelectItem<T> GetCurrentSelectItem<T>()
        {
            return (SelectItem<T>)_selectItems[SelectItemIndex];
        }

        private void MoveLeft()
        {
            Move(-1);
        }

        private void MoveRight()
        {
            Move(1);
        }

        private void Move(int offset)
        {
            var newIndex = SelectItemIndex + offset;

            if (newIndex < 0 || newIndex > _selectItems.Count - 1)
                return;

            SelectItemIndex = newIndex;

            _view.SetItemText(_selectItems[SelectItemIndex].Title);
            SetButtonsStates();

            OnValueChangeEvent?.Invoke(_selectItems[SelectItemIndex], SelectItemIndex);
        }

        private void SetButtonsStates()
        {
            _leftButton.gameObject.SetActive(SelectItemIndex != 0);
            _rightButton.gameObject.SetActive(SelectItemIndex != _selectItems.Count - 1);
        }

        private void Awake()
        {
            _leftButton.onClick.AddListener(MoveLeft);
            _rightButton.onClick.AddListener(MoveRight);
        }

        private void OnDestroy()
        {
            _leftButton.onClick?.RemoveListener(MoveLeft);
            _rightButton.onClick?.RemoveListener(MoveRight);
        }
    }
}