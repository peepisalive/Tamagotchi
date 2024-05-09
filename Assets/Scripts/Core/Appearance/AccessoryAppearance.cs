using UnityEngine;

namespace Core
{
    public sealed class AccessoryAppearance : MonoBehaviour
    {
        [field: SerializeField] public AccessoryType Type { get; private set; }
    }
}