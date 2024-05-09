using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public sealed class PetAppearance : MonoBehaviour
    {
        [field: SerializeField] public PetType Type { get; private set; }
        [field: SerializeField] public List<AccessoryAppearance> AccessoriesAppearances { get; private set; }
    }
}