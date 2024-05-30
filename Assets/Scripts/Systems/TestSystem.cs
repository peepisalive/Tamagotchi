#if UNITY_EDITOR
using Leopotam.Ecs;
using UnityEngine;
using Components;
using System;

namespace Systems
{
    public sealed class TestSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        public void Run()
        {
            PerformAction(KeyCode.P, KeyCode.D, () =>
            {
                _world.NewEntity().Replace(new DeathEvent());
            });
        }

        private void PerformAction(KeyCode keyPressed, KeyCode keyUp, Action callback)
        {
            if (!Input.GetKey(keyPressed))
                return;

            if (!Input.GetKeyUp(keyUp))
                return;

            callback?.Invoke();
        }
    }
}
#endif