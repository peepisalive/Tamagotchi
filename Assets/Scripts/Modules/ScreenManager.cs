using System.Collections.Generic;
using UI.Screen.Controller;
using UnityEngine;
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
            //_screens = SettingsProvider.Get<PrefabSet>().Screens;
        }

        public void ReplacePreviousScreen(Type screenControllerType, Vector2 showDirection, Vector2 hideDirection, bool overPrevious = true)
        {
            // to do
            Debug.Log($"show screen {screenControllerType.Name}");
        }

        private void ShowScreen<TController>(TController prefab, Vector2 direction, bool overPrevious = true) where TController: ScreenController
        {
            // to do
        }
    }
}