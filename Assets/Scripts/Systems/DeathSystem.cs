using Application = Tamagotchi.Application;
using System.Collections.Generic;
using Leopotam.Ecs;
using UI.Settings;
using Components;
using UI.Popups;
using Settings;
using Modules;
using Utils;
using Core;

namespace Systems
{
    public sealed class DeathSystem : IEcsRunSystem, IAdRewardable
    {
        private EcsWorld _world;
        private EcsFilter<DeathEvent> _deathFilter;
        private EcsFilter<PetComponent> _petFilter;

        private DeathSettings _settings;

        public void Run()
        {
            if (_deathFilter.IsEmpty())
                return;

            HandleDeath();
        }

        public void OnAdFailedToShow()
        {
            PopupUtils.ShowNoAdsAvailablePopup();
        }

        public void OnRewarded()
        {
            foreach (var i in _petFilter)
            {
                _petFilter.Get1(i).Pet.Parameters.Get(ParameterType.Health).Add(1f);
                _petFilter.GetEntity(i).Del<DeadComponent>();
            }
        }

        private void HandleDeath()
        {
            _settings ??= SettingsProvider.Get<DeathSettings>();

            foreach (var i in _petFilter)
            {
                var pet = _petFilter.Get1(i).Pet;
                var navigationPoint = Application.Model.GetCurrentNavigationPoint();
                var usePetIcon = !_settings.PetIconExcludingTypes.Contains(navigationPoint.Type);

                _petFilter.GetEntity(i).Replace(new DeadComponent());
                _world.NewEntity().Replace(new ShowPopupComponent
                {
                    Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                    {
                        Title = "[test]",
                        Content = "[test]",
                        ButtonSettings = new List<TextButtonSettings>
                        {
                            new TextButtonSettings
                            {
                                Title = "[test]",
                                Action = () =>
                                {
                                    // to do: select new pet
                                }
                            },
                            new TextButtonSettings
                            {
                                Title = "[test]",
                                Action = () =>
                                {
                                    RewardedAdManager.Instance.OnAdFailedToShowCallback += OnAdFailedToShow;
                                    RewardedAdManager.Instance.OnRewardedCallback += OnRewarded;
                                    RewardedAdManager.Instance.ShowRewardedAd();

                                    _world.NewEntity().Replace(new HidePopupComponent());
                                },
                                AdsSignState = true
                            }
                        },
                        UsePetIcon = usePetIcon,
                        IgnoreOverlayButton = true
                    })
                });
            }
        }
    }
}