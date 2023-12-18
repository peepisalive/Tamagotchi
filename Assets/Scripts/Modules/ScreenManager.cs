using System.Collections.Generic;
using UI.Screen.Controller;
using UnityEngine;
using System.Linq;
using Settings;
using System;

namespace Modules
{
    public sealed class ScreenManager
    {
        private Transform ScreenParent
        {
            get
            {
                if (_screenParent == null)
                    _screenParent = GameObject.FindGameObjectWithTag("ScreenParent").transform;

                return _screenParent;
            }
        }

        private ScreenController _previousScreen;
        private ScreenController _currentScreen;
        private List<ScreenController> _screens;

        private Transform _screenParent;

        public ScreenManager()
        {
            _screens = SettingsProvider.Get<PrefabsSet>().Screens;
        }

        public void ReplacePreviousScreen(Type screenControllerType, Vector2 showDirection, Vector2 hideDirection, bool overPrevious = true)
        {
            if (!typeof(ScreenController).IsAssignableFrom(screenControllerType))
            {
#if UNITY_EDITOR
                Debug.LogError("ScreenManager error: \"typeof(ScreenController).IsAssignableFrom(controllerType) = false\"");
#endif
                return;
            }

            var screen = _screens.FirstOrDefault(s => s.GetComponent<ScreenController>().GetType() == screenControllerType);

            if (screen == null)
            {
#if UNITY_EDITOR
                Debug.LogError("ScreenManager error: \"screen to show is null.\"");
#endif
                return;
            }

            if (_currentScreen != null)
            {
                _previousScreen = _currentScreen;
            }

            ShowScreen(screen, showDirection, overPrevious);

            Debug.Log($"show screen {screenControllerType.Name}");
        }

        private void ShowScreen<TController>(TController prefab, Vector2 direction, bool overPrevious = true) where TController: ScreenController
        {
            var screen = GameObject.Instantiate(prefab, ScreenParent);

            if (overPrevious)
            {
                screen.transform.SetAsLastSibling();
            }
            else
            {
                screen.transform.SetAsFirstSibling();
            }

            screen.Show(direction, DestroyPreviousScreen);
            _currentScreen = screen;
            // send event
        }

        private void DestroyPreviousScreen()
        {
            if (_previousScreen == null)
                return;

            GameObject.Destroy(_previousScreen.gameObject);
        }
    }
}