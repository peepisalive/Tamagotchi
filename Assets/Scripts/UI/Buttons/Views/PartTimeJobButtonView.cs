using UnityEngine;
using UI.View;

public sealed class PartTimeJobButtonView : JobButtonView
{
    [Space(10)]
    [SerializeField] private RectTransform _adsParent;

    public void SetAdsState(bool state)
    {
        _adsParent.gameObject.SetActive(state);
    }
}
