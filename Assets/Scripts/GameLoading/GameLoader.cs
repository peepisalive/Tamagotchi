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

            SceneManager.LoadScene("MainScene", LoadSceneMode.Single); // to do: edit this, cringe
        } 

        private void Awake()
        {
            StartCoroutine(LoadGame());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}