using GameLoading.LoadingOperations;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace GameLoading
{
    public sealed class GameLoader : MonoBehaviour
    {
        [SerializeField] private List<LoadingOperationsPack> _operationPacks;

        private IEnumerator LoadGame()
        {
            if (_operationPacks == null || !_operationPacks.Any())
                yield break;

            foreach (var pack in _operationPacks)
            {
                pack.Begin();

                while (!pack.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                }
            }

            SceneManager.LoadScene("MainScene", LoadSceneMode.Single); // TO DO: edit this, cringe
        } 

        private void Awake()
        {
#if !UNITY_EDITOR
            Application.targetFrameRate = 60;
#endif
            StartCoroutine(LoadGame());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}