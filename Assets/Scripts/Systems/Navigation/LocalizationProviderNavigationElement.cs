using UnityEngine.Localization.Settings;
using Components.Modules.Navigation;
using System.Collections.Generic;
using Modules.Localization;
using Modules.Navigation;
using Leopotam.Ecs;
using UI.Settings;
using System.Linq;
using UnityEngine;
using Components;
using UI.Popups;
using Modules;
using System;
using Events;
using Utils;

namespace Systems.Navigation
{
    public sealed class LocalizationProviderNavigationElement : IEcsInitSystem, INavigationElement
    {
        public HashSet<NavigationElementType> Types => new HashSet<NavigationElementType>
        {
            NavigationElementType.LocalizationProvider
        };

        private EcsWorld _world;
        private EcsFilter<BlockComponent> _blockFilter;

        public void Init()
        {
            _blockFilter.RegisterElement(NavigationBlockType.Main, this);
        }

        public bool CanDisplay(NavigationElementType elementType)
        {
            return true;
        }

        public bool IsEnable(NavigationElementType elementType)
        {
            return true;
        }

        public bool NotificationIsEnable(NavigationElementType elementType)
        {
            return false;
        }

        public bool OnClick(NavigationElementType elementType)
        {
            var buttonData = _blockFilter.GetNavigationButtonData(NavigationBlockType.Main, elementType, this);

            _world.NewEntity().Replace(new ShowPopupComponent
            {
                Settings = new PopupToShow<DefaultPopup>(new DefaultPopup
                {
                    Title = buttonData.Title,
                    Icon = buttonData.Icon,
                    Content = buttonData.Description,
                    DropdownSettings = GetDropdownSettings(),
                    ButtonSettings = new List<TextButtonSettings>
                    {
                        new TextButtonSettings
                        {
                            Title = LocalizationProvider.GetText("cancel/button"),
                            Action = () =>
                            {
                                _world.NewEntity().Replace(new HidePopupComponent());
                            }
                        },
                        new TextButtonSettings
                        {
                            Title = LocalizationProvider.GetText("ok/button"),
                            ActionWithInstance = async (popup) =>
                            {
                                var defaultPopup = (DefaultPopupView)popup;
                                var selectedLanguage = defaultPopup.Dropdowns[0].GetCurrentValue<LanguageType>();
                                var localeCode = LocalizationProvider.GetLocaleCode(selectedLanguage);
                                var locale = LocalizationSettings.AvailableLocales.Locales.First(locale => locale.Identifier.Code == localeCode);

                                LocalizationProvider.OnInitializeEvent += OnChangeLanguage;

                                PlayerPrefs.SetString("selected-locale", localeCode);
                                await LocalizationProvider.Initialize(locale);
                            }
                        }
                    },
                    IgnoreOverlayButton = true,
                    UseIcon = true
                })
            });

            return false;
        }

        public NavigationButtonData GetButtonData(NavigationElementType elementType)
        {
            return _blockFilter.GetNavigationButtonData(NavigationBlockType.Main, elementType, this);
        }

        public NavigationScreenData GetScreenData(NavigationElementType elementType)
        {
            return _blockFilter.GetNavigationScreenData(NavigationBlockType.Main, elementType);
        }

        private void OnChangeLanguage()
        {
            LocalizationProvider.OnInitializeEvent -= OnChangeLanguage;

            EventSystem.Send(new UpdateCurrentScreenEvent());
            _world.NewEntity().Replace(new HidePopupComponent());
        }

        private List<DropdownSettings> GetDropdownSettings()
        {
            var types = Enum.GetValues(typeof(LanguageType)).Cast<LanguageType>();
            var dropdownSettings = new List<DropdownSettings>
            {
                new DropdownSettings
                {
                    Title = LocalizationProvider.GetText("language"),
                    DropdownContent = new List<DropdownContent>()
                }
            };

            foreach (var type in types)
            {
                dropdownSettings.First().DropdownContent.Add(new DropdownContent<LanguageType>
                {
                    Title = LocalizationProvider.GetText($"language/{type}"),
                    Value = type
                });
            }

            return dropdownSettings;
        }
    }
}