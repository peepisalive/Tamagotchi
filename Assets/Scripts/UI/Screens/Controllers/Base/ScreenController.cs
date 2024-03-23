using Application = Tamagotchi.Application;
using UnityEngine;
using DG.Tweening;
using System;

namespace UI.Controller.Screen
{
    public class ScreenController : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] private NavigationPanel _navigationPanel;

        private const float ANIMATION_DURATION = .2f;

        public virtual void Setup()
        {
            _navigationPanel?.Setup();
        }

        public void Show(Vector2 direction, Action onComplete = null)
        {
            DoShowAnimation(direction, onComplete);
        }

        public void Hide(Vector2 direction, Action onComplete = null)
        {
            DoHideAnimation(direction, onComplete);
        }

        private void DoShowAnimation(Vector2 direction, Action onComplete = null)
        {
            var targetPosition = transform.localPosition;

            SetStartScreenPosition(direction);
            DoAnimation(targetPosition, onComplete);


            void SetStartScreenPosition(Vector2 direction)
            {
                var startPosition = CalculateOffset(direction * -1);

                if (Mathf.Abs(startPosition.sqrMagnitude) - Mathf.Abs(Vector2.zero.sqrMagnitude) <= Mathf.Epsilon)
                    return;

                transform.localPosition += startPosition;
            }
        }

        private void DoHideAnimation(Vector2 direction, Action onComplete = null)
        {
            DoAnimation(CalculateOffset(direction), onComplete);
        }

        private void DoAnimation(Vector3 targetPosition, Action onComplete = null)
        {
            transform.DOLocalMove(targetPosition, ANIMATION_DURATION)
                .SetEase(Ease.InOutCubic)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }

        private Vector3 CalculateOffset(Vector2 direction)
        {
            if ((direction - Vector2.left).sqrMagnitude < Mathf.Epsilon
                || (direction - Vector2.right).sqrMagnitude < Mathf.Epsilon)
            {
                return direction.normalized * Application.MainCanvas.sizeDelta.x;
            }

            if ((direction - Vector2.up).sqrMagnitude < Mathf.Epsilon
                || (direction - Vector2.down).sqrMagnitude < Mathf.Epsilon)
            {
                return direction.normalized * Application.MainCanvas.sizeDelta.y;
            }

            return Vector3.zero;
        }
    }
}