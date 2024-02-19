using UnityEngine;
using UI.View;

namespace UI.Controller
{
    [RequireComponent(typeof(BarView))]
    public sealed class BarController : MonoBehaviour
    {
        [SerializeField] private BarView _view;

        public void Setup()
        {

        }
    }
}