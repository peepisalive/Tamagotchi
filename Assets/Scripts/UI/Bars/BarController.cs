using UnityEngine;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(BarView))]
    public class BarController : MonoBehaviour
    {
        [SerializeField] private BarView _view;

        public void Setup(float value, Sprite icon, bool invertColor = false)
        {
            SetValue(value, invertColor);
            _view.SetIcon(icon);
        }

        protected void SetValue(float value, bool invertColor = false)
        {
            _view.SetFillValue(value, invertColor);
        }
    }
}