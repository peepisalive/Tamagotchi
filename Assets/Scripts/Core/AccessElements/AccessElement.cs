using UnityEngine;

namespace Core
{
    public class AccessElement
    {
        [field: SerializeField] public AccessType AccessType { get; private set; }

        public bool IsUnlocked { get; private set; }
        public bool IsCurrent { get; private set; }

        public void SetCurrentState(bool state)
        {
            IsCurrent = state;
        }

        public void Unlock()
        {
            IsUnlocked = true;
        }
    }
}