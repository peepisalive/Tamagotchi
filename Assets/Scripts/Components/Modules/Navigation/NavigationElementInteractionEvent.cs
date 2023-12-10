using Modules.Navigation;

namespace Components.Modules.Navigation
{
    public struct NavigationElementInteractionEvent
    {
        public NavigationElementType Type;
        public INavigationElement Element;

        public bool IsTransition;
    }
}