using UnityEngine;

namespace Core
{
    public sealed class PetAppearance : MonoBehaviour
    {
        [field: SerializeField] public PetType Type { get; private set; }
    }
}