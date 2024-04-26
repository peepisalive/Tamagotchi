using UnityEngine;

namespace UI.View
{
    public class ButtonView : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] private RectTransform _adsSignParent;

        public void SetAdsSignState(bool state)
        {
            _adsSignParent.gameObject.SetActive(state);
        }
    }
}