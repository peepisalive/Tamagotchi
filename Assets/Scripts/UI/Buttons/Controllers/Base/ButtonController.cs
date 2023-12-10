using UnityEngine.UI;
using UI.Settings;
using UnityEngine;

namespace UI.Controller
{
    [RequireComponent(typeof(Button))]
    public class ButtonController : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] private Button _button;

        public virtual void Setup(ButtonSettings settings)
        {
            _button?.onClick.AddListener(() => settings.Action?.Invoke());
        }

        private void RemoveAllListeners()
        {
            _button?.onClick?.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            RemoveAllListeners();
        }
    }
}