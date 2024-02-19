using UnityEngine;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(BarView))]
    public class BarController : MonoBehaviour
    {
        [SerializeField] private BarView _view;

        public void Setup(float value, Sprite icon)
        {
            SetValue(value);
            _view.SetIcon(icon);
        }

        protected void SetValue(float value, float previousValue = 0f)
        {
            _view.SetFillValue(value);
        }
    }
}