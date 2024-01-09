using Leopotam.Ecs;
using UnityEngine;

namespace Starter
{
    public abstract class EcsProcessing : MonoBehaviour
    {
        protected EcsSystems Systems;

        public abstract void InitSystems(EcsWorld world);

        public void RunSystems()
        {
            Systems?.Run();
        }

        public void DestroySystems()
        {
            Systems?.Destroy();
            Systems = null;
        }
    }
}