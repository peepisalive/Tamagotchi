using UnityEngine;

namespace Scenes
{
    public sealed class InitializationScene : Scene
    {
        public static string Name => "InitializationScene";

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