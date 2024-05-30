using UnityEngine;

namespace Scenes
{
    public sealed class MainScene : Scene
    {
        public static string Name => "MainScene";

        public static AsyncOperation LoadScene()
        {
            return LoadScene(Name);
        }

        public static AsyncOperation LoadScene<T>(T argument)
        {
            return LoadScene(Name, argument);
        }
    }
}