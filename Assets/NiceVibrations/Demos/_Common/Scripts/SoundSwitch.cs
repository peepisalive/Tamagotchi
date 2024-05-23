using UnityEngine;

namespace MoreMountains.NiceVibrations
{
    public class SoundSwitch : MonoBehaviour
    {
        public V2DemoManager DemoManager;

        protected MMSwitch _switch;

        protected virtual void Awake()
        {
            _switch = this.gameObject.GetComponent<MMSwitch>();
        }

        protected virtual void OnEnable()
        {
            _switch.CurrentSwitchState = (DemoManager.SoundActive)
                ? MMSwitch.SwitchStates.On : MMSwitch.SwitchStates.Off;
            _switch.InitializeState();
        }
    }
}
