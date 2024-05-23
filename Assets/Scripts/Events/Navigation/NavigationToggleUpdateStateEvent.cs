using Modules.Navigation;

namespace Events
{
    public sealed class NavigationToggleUpdateStateEvent
    {
        public NavigationElementType Type;
        public bool State;
    }
}