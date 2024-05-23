using System.Collections.Generic;
using UI.Controller.Screen;
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

        [field: Header("Navigation elements")]
        [field: SerializeField] public NavigationButtonController NavigationButton { get; private set; }

        [field: Header("Info elements")]
        [field: SerializeField] public InfoFieldController InfoField { get; private set; }
        [field: SerializeField] public DropdownController Dropdown { get; private set; }
        [field: SerializeField] public BarController InfoBar { get; private set; }

        [field: Header("Pet appearance")]
        [field: SerializeField] public PetCamera PetCamera { get; private set; }
    }
}