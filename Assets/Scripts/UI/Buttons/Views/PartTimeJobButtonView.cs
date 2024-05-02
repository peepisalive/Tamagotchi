using UnityEngine;
using UI.View;

public sealed class PartTimeJobButtonView : JobButtonView
{
    [Space(10)]
    [SerializeField] private RectTransform _adParent;

    public void SetAdState(bool state)
    {
        _adParent.gameObject.SetActive(state);
    }
}
