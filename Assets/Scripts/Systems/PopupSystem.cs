using Leopotam.Ecs;
using Components;
using Modules;
using Events;

namespace Systems
{
    public sealed class PopupSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsFilter<ShowPopup> _showPopupFilter;
        private EcsFilter<HidePopup> _hidePopupFilter;

        private PopupViewManager _popupViewManager;

        public void Init()
        {
            _popupViewManager = new PopupViewManager();

            EventSystem.Subscribe<ShowPopupEvent>(ShowPopup);
            EventSystem.Subscribe<HidePopupEvent>(HidePopup);
        }

        public void Run()
        {
            foreach (var i in _hidePopupFilter)
            {
                HidePopup();
                _hidePopupFilter.GetEntity(i).Destroy();
            }

            foreach (var i in _showPopupFilter)
            {
                ShowPopup(new ShowPopupEvent
                {
                    Settings = _showPopupFilter.Get1(i).Settings
                });
                _showPopupFilter.GetEntity(i).Destroy();
            }
        }

        public void Destroy()
        {
            EventSystem.Unsubscribe<ShowPopupEvent>(ShowPopup);
            EventSystem.Unsubscribe<HidePopupEvent>(HidePopup);
        }

        private void ShowPopup(ShowPopupEvent e = null)
        {
            e.Settings.ShowPopup(_popupViewManager);
        }

        private void HidePopup(HidePopupEvent e = null)
        {
            _popupViewManager.HideCurrentPopup();
        }
    }
}