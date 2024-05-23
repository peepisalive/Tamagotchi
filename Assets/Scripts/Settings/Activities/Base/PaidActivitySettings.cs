using Modules.Navigation;
using UnityEngine;

namespace Settings.Activity
{
    [CreateAssetMenu(fileName = "PaidActivitySettings", menuName = "Settings/Activities/PaidActivitySettings", order = 1)]
    public sealed class PaidActivitySettings : ActivitySettings
    {
        public override NavigationElementType Type => _type;
        public int Price => -_price;

        [SerializeField] private NavigationElementType _type;
        [SerializeField][Range(0, 2000)] private int _price;
    }
}