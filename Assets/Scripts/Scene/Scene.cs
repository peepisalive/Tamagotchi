using UnityEngine.SceneManagement;
using UnityEngine;

namespace Scenes
{
    public abstract class Scene
    {
        protected static AsyncOperation LoadScene(string name, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            return SceneManager.LoadSceneAsync(name, loadMode);
        }

        protected static AsyncOperation LoadScene<T>(string name, T argument, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            LoadSceneProcessor.Instance.RegisterLoadAction(argument);
            return SceneManager.LoadSceneAsync(name, loadMode);
        }
    }
}