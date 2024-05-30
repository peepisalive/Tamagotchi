using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using System;

namespace Scenes
{
    public sealed class LoadSceneProcessor : MonoBehaviour
    {
        public static LoadSceneProcessor Instance
        {
            get
            {
                if (_instance == null)
                    Initialize();

                return _instance;
            }
        }

        private static LoadSceneProcessor _instance;
        private Action _loadAction;

        public void RegisterLoadAction<T>(T argument)
        {
            _loadAction = () =>
            {
                var objects = SceneManager.GetActiveScene().GetRootGameObjects();
                var handler = objects.First(o => o.GetComponentInChildren<ISceneLoadHandler<T>>() != null);

                handler.GetComponentInChildren<ISceneLoadHandler<T>>().OnSceneLoad(argument);
            };
        }

        public void InvokeLoadAction()
        {
            _loadAction?.Invoke();
            _loadAction = null;
        }

        private static void Initialize()
        {
            _instance = new GameObject("LoadSceneProcessor").AddComponent<LoadSceneProcessor>();
            _instance.transform.SetParent(null);

            DontDestroyOnLoad(_instance);
        }
    }
}