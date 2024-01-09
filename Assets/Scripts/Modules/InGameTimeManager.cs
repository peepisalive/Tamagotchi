using Application = Tamagotchi.Application;
using UnityEngine;
using Save.State;

namespace Modules
{
    public sealed class InGameTimeManager : MonoBehaviour
    {
        private GlobalStateHolder _stateHolder;

        private void Update()
        {
            _stateHolder.State.PlayTimeSeconds += Time.deltaTime;
        }

        private void Start()
        {
            _stateHolder = Application.SaveDataManager.GetStateHolder<GlobalStateHolder>();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {

            }
        }
    }
}