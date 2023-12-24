using UI.Popups;
using Modules;

public sealed class PopupToShow<T> : PopupToShow where T : Popup
{
    private T _popup;

    public PopupToShow(T popup)
    {
        _popup = popup;
    }

    public override void ShowPopup(PopupViewManager popupViewManager)
    {
        if (popupViewManager == null)
            return;

        popupViewManager.ShowPopup(_popup);
    }
}

public abstract class PopupToShow
{
    public abstract void ShowPopup(PopupViewManager popupViewManager);
}