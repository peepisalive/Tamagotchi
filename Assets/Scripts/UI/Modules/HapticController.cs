using MoreMountains.NiceVibrations;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Modules;

namespace UI.Controllers
{
    [RequireComponent(typeof(Button))]
    public sealed class HapticController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private HapticTypes _hapticType;

        public void OnPointerDown(PointerEventData eventData)
        {
            HapticProvider.Instance.Haptic(_hapticType);
        }
    }
}