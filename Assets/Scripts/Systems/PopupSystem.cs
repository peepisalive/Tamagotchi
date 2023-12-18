using Modules.Managers;
using Leopotam.Ecs;
using Components;

namespace Systems
{
    public sealed class PopupSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<ShowPopup> _showPopupFilter;
        private EcsFilter<HidePopup> _hidePopupFilter;

        private PopupViewManager _popupViewManager;

        public void Init()
        {
            _popupViewManager = new PopupViewManager();
        }

        public void Run()
        {
            foreach (var i in _hidePopupFilter)
            {
                _popupViewManager.HideCurrentPopup();
                _hidePopupFilter.GetEntity(i).Destroy();
            }

            foreach (var i in _showPopupFilter)
            {
                _showPopupFilter.Get1(i).Settings.ShowPopup(_popupViewManager);
                _showPopupFilter.GetEntity(i).Destroy();
            }
        }
    }
}