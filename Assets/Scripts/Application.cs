using UnityEngine;
using Modules;

namespace Tamagotchi
{
    public static class Application
    {
        public static SaveDataManager SaveDataManager { get; private set; }
        public static Model Model { get; private set; }

        public static RectTransform MainCanvas
        {
            get
            {
                if (_mainCanvas == null)
                    _mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();

                return _mainCanvas;
            }
        }

        private static RectTransform _mainCanvas;

        static Application()
        {
            Model = new Model();
            SaveDataManager = new SaveDataManager();
        }
    }
}