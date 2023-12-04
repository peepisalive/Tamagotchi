using UnityEngine;
using UI.View;
using Core;

namespace UI.Controller
{
    [RequireComponent(typeof(ParameterBarView))]
    public class ParameterBarController : MonoBehaviour
    {
        [SerializeField] private ParameterBarView _view;

        public void Setup(float value, Parameter parameter)
        {

        }
    }
}