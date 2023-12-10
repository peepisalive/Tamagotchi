using UnityEngine;

namespace Tamagotchi
{
    public static class Application
    {
        public static Model Model { get; private set; }
        public static RectTransform MainCanvas
        {
            get
            {
                if (_screenParent == null)
                    _screenParent = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();

                return _screenParent;
            }
        }

        private static RectTransform _screenParent;

        static Application()
        {
            Model = new Model();
        }
    }
}