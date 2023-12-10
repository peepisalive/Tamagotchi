using Modules.Navigation;

namespace Components.Modules.Navigation
{
    public struct NavigationPointChangedEvent
    {
        public NavigationPoint PreviousPoint;
        public NavigationPoint CurrentPoint;

        public TransitionType TransitionType;
    }
}