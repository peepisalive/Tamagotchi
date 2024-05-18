using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Core;

namespace Settings
{
    [CreateAssetMenu(fileName = "PetAppearanceSettings", menuName = "Settings/PetAppearanceSettings", order = 0)]
    public sealed class PetAppearanceSettings : ScriptableObject
    {
        [SerializeField] private List<PetAppearanceController> _appearances;

        public PetAppearanceController GetAppearance(PetType type)
        {
            return _appearances.FirstOrDefault(a => a.Type == type);
        }
    }
}