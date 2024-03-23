using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Modules;
using Core;

namespace UI.Controller
{
    [RequireComponent(typeof(Button))]
    public sealed class SoundController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private SoundType _soundType;

        public void OnPointerDown(PointerEventData eventData)
        {
            SoundProvider.Instance.PlaySoundEffect(_soundType);
        }
    }
}