using Leopotam.Ecs;
using UnityEngine;

namespace Core
{
    public sealed class GameProcessingEcs : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        private void InitSystems()
        {
            _systems.Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            InitSystems();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems = null;

            _world?.Destroy();
            _world = null;
        }
    }
}