namespace Modules.Navigation
{
    public sealed class NavigationPoint
    {
        public NavigationElementType Type { get; private set; }
        public INavigationElement Element { get; private set; }

        public NavigationPoint(NavigationElementType type, INavigationElement navigationElement)
        {
            Type = type;
            Element = navigationElement;
        }
    }
}