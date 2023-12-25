using System.Collections.Generic;
using UI.Screen.Controller;
using UI.Controller;
using UnityEngine;
using UI.Popups;
using Core;

namespace Settings
{
    [CreateAssetMenu(fileName = "PrefabsSet", menuName = "Settings/PrefabsSet", order = 0)]
    public sealed class PrefabsSet : ScriptableObject
    {
        [field: SerializeField] public List<ButtonController> Buttons { get; private set; }
        [field: SerializeField] public List<ScreenController> Screens { get; private set; }
        [field: SerializeField] public List<PopupViewBase> Popups { get; private set; }

        [field: Header("Pet appearance")]
        [field: SerializeField] public PetAppearance PetAppearance { get; private set; }
        [field: SerializeField] public PetCamera PetCamera { get; private set; }
    }
}