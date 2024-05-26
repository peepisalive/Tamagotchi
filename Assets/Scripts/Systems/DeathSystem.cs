using Application = Tamagotchi.Application;
using System.Collections.Generic;
using Core.Animation;
using Leopotam.Ecs;
using UI.Settings;
using Components;
using UI.Popups;
using Settings;
using Modules;
using Utils;
using Core;
using Modules.Localization;

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
            PopupUtils.ShowNoAdsAvailablePopup(() =>
            {
                foreach (var i in _petFilter)
                {
                    var pet = _petFilter.Get1(i).Pet;
                    ShowDeathPopup(pet.Type, pet.Name);
                }
            });
        }

        public void OnRewarded()
        {
            foreach (var i in _petFilter)
            {
                _petFilter.Get1(i).Pet.Parameters.Get(ParameterType.Health).Add(1f);
                _petFilter.GetEntity(i).Del<DeadComponent>();
            }

            _world.NewEntity().Replace(new ChangePetAnimationEvent(default));
            _world.NewEntity().Replace(new ChangePetEyesAnimationEvent(default));
        }

        private void HandleDeath()
        {
            _settings ??= SettingsProvider.Get<DeathSettings>();

            foreach (var i in _petFilter)
            {
                _petFilter.GetEntity(i).Replace(new DeadComponent());

                var pet = _petFilter.Get1(i).Pet;

                _world.NewEntity().Replace(new ChangePetAnimationEvent(AnimationType.Death));
                _world.NewEntity().Replace(new ChangePetEyesAnimationEvent(EyesAnimationType.Dead));

                ShowDeathPopup(pet.Type, pet.Name);
            }
        }

        private void ShowDeathPopup(PetType type, string petName)
        {
            var navigationPoint = Application.Model.GetCurrentNavigationPoint();
            var usePetIcon = !_settings.PetIconExcludingTypes.Contains(navigationPoint.Type);

            _world.NewEntity().Replace(new ChangePetAnimationEvent(AnimationType.Death));
            _world.NewEntity().Replace(new ChangePetEyesAnimationEvent(EyesAnimationType.Dead));

            _world.NewEntity().Replace(new ShowPopupComponent
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = _settings.Localization.PopupTitle,
                    Content = _settings.Localization.PopupContent,
                    InfoFieldSettings = new List<InfoFieldSettings>
                    {
                        new InfoFieldSettings
                        {
                            Title = LocalizationProvider.GetText($"pet/{type}"),
                            Content = petName
                        }
                    },
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Title = _settings.Localization.NewPetButtonTitle,
                            Action = () =>
                            {
                                // to do: select new pet
                            }
                        },
                        new TextButtonSettings
                        {
                            Title = _settings.Localization.ResurrectButtonTitle,
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