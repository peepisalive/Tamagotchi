using Leopotam.Ecs;
using UnityEngine;

namespace Starter
{
    public sealed class GameStarter : MonoBehaviour
    {
        private EcsWorld _world;

        [SerializeField] private EcsProcessing _navigationProcessing;
        [SerializeField] private EcsProcessing _gameProcessing;
        [SerializeField] private EcsProcessing _saveProcessing;

        private void Update()
        {
            _navigationProcessing.RunSystems();
            _gameProcessing.RunSystems();
        }

        private void Awake()
        {
            _world = new EcsWorld();

            _navigationProcessing.InitSystems(_world);
            _saveProcessing.InitSystems(_world);
            _gameProcessing.InitSystems(_world);
        }

        private void OnDestroy()
        {
            _navigationProcessing.DestroySystems();
            _gameProcessing.DestroySystems();
            _saveProcessing.DestroySystems();

            _world?.Destroy();
            _world = null;
        }
    }
}