using System.Collections.Generic;
using Modules.Navigation;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "DeathSettings", menuName = "Settings/DeathSettings", order = 0)]
    public sealed class DeathSettings : ScriptableObject
    {
        [field: SerializeField] public List<NavigationElementType> PetIconExcludingTypes { get; private set; }
    }
}