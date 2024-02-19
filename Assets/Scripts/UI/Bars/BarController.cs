using UnityEngine;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(BarView))]
    public class BarController : MonoBehaviour
    {
        [SerializeField] private BarView _view;

        protected void SetValue(float value, float previousValue)
        {
            _view.SetFillValue(value);
        }
    }
}