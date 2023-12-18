using System.Collections.Generic;
using UI.Screen.Controller;
using UnityEngine;
using UI;

namespace Settings
{
    [CreateAssetMenu(fileName = "PrefabsSet", menuName = "Settings/PrefabsSet", order = 0)]
    public sealed class PrefabsSet : ScriptableObject
    {
        [field: SerializeField] public List<ScreenController> Screens { get; private set; }
        [field: SerializeField] public List<PopupViewBase> Popups { get; private set; }
    }
}