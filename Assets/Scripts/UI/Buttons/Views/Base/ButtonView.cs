using UnityEngine;

namespace UI.View
{
    public class ButtonView : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] private GameObject _adsSignParent;

        public void SetAdsSignState(bool state)
        {
            _adsSignParent.SetActive(state);
        }
    }
}