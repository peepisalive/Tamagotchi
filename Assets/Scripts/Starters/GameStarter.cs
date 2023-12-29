using Leopotam.Ecs;
using UnityEngine;

namespace Starter
{
    public class GameStarter : MonoBehaviour
    {
        private EcsWorld _world;

        [SerializeField] private Starter _navigationStater;
        [SerializeField] private Starter _processingEcsStarter;

        private void Update()
        {
            _processingEcsStarter.RunSystems();
            _navigationStater.RunSystems();
        }

        private void Awake()
        {
            _world = new EcsWorld();

            _processingEcsStarter.InitSystems(_world);
            _navigationStater.InitSystems(_world);
        }

        private void OnDestroy()
        {
            _processingEcsStarter.DestroySystems();
            _navigationStater.DestroySystems();

            _world?.Destroy();
            _world = null;
        }
    }
}