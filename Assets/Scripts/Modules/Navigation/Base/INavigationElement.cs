using Components;
using System.Collections.Generic;

namespace Modules.Navigation
{
    public interface INavigationElement
    {
        public HashSet<NavigationElementType> Types { get; }

        public bool NotificationIsEnable(NavigationElementType elementType);
        public bool CanDisplay(NavigationElementType elementType);
        public bool IsEnable(NavigationElementType elementType);
        public bool OnClick(NavigationElementType elementType);

        public NavigationButtonData GetButtonData(NavigationElementType elementType);
        public NavigationScreenData GetScreenData(NavigationElementType elementType);
    }
}