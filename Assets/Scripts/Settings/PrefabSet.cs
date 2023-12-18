using System.Collections.Generic;
using UI.Screen.Controller;
using UnityEngine;
using Core;

namespace Settings
{
    [CreateAssetMenu(fileName = "PrefabSet", menuName = "Settings/PrefabSet", order = 0)]
    public sealed class PrefabSet : ScriptableObject
    {
        [field: SerializeField] public List<ScreenController> Screens { get; private set; }

        [field: Header("Pet appearance")]
        [field: SerializeField] public Camera PetCamera { get; private set; }
        [field: SerializeField] public PetAppearance PetAppearance { get; private set; }
    }
}