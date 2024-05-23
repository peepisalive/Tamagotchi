using UnityEngine;

namespace Core
{
    public class AccessElement
    {
        public AccessType AccessType { get; private set; }

        public bool IsUnlocked { get; private set; }
        public bool IsCurrent { get; private set; }

        public AccessElement(AccessType accessType, bool isUnlocked, bool isCurrent)
        {
            AccessType = accessType;
            IsUnlocked = isUnlocked;
            IsCurrent = isCurrent;
        }

        public void SetUnlockState(bool state)
        {
            IsUnlocked = state;
        }

        public void SetCurrentState(bool state)
        {
            IsCurrent = state;
        }
    }
}