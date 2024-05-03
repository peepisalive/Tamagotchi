using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace UI.Controller
{
    public sealed class PickerController : MonoBehaviour, IDragHandler
    {
        public event Action<float, float> OnPickerChangePositionEvent;

        [SerializeField] private RectTransform _parentRect;
        [SerializeField] private RectTransform _pickerRect;

        private Canvas _mainCanvas;
        private const float OFFSET = 25f;

        public void OnDrag(PointerEventData eventData)
        {
            UpdatePosition(eventData);
        }

        private void UpdatePosition(PointerEventData eventData)
        {
            var position = _pickerRect.anchoredPosition + (eventData.delta / _mainCanvas.scaleFactor);
            var deltaX = _parentRect.sizeDelta.x * 0.5f - OFFSET;
            var deltaY = _parentRect.sizeDelta.y * 0.5f - OFFSET;

            position.x = Mathf.Clamp(position.x, -deltaX, deltaX);
            position.y = Mathf.Clamp(position.y, -deltaY, deltaY);

            _pickerRect.anchoredPosition = position;

            var xNormalized = (position.x + deltaX) / _parentRect.sizeDelta.x;
            var yNormalized = (position.y + deltaY) / _parentRect.sizeDelta.y;

            OnPickerChangePositionEvent?.Invoke(xNormalized, yNormalized);
        }

        private void Awake()
        {
            _pickerRect.localPosition = new Vector2(-(_parentRect.sizeDelta.x * 0.5f) + OFFSET, -(_parentRect.sizeDelta.y * 0.5f) + OFFSET);
            _mainCanvas = Tamagotchi.Application.MainCanvas.GetComponent<Canvas>();
        }
    }
}