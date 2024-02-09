using Modules.Navigation;
using UnityEngine;
using Core;

namespace Settings.Activities
{
    public abstract class ActivitySettings : ScriptableObject
    {
        public abstract NavigationElementType Type { get; }
        public int Price => -_price;

        [SerializeField][Range(0, 1500)] private int _price;
    }
}