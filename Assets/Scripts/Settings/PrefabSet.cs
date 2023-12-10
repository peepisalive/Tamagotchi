using System.Collections.Generic;
using UI.Screen.Controller;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PrefabSet", menuName = "Settings/PrefabSet", order = 0)]
    public sealed class PrefabSet : ScriptableObject
    {
        [field: SerializeField] public List<ScreenController> Screens { get; private set; }
    }
}