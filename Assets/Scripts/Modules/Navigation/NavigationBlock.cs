using Settings.Modules.Navigations;
using System.Collections.Generic;
using System.Linq;

namespace Modules.Navigation
{
    public sealed class NavigationBlock
    {
        public NavigationBlockType Type { get; private set; }
        public NavigationElementType RootElementType { get; private set; }

        public NavigationPoint CurrentPoint { get; private set; }
        public bool IsEmptyNavigationChain => _navigationChain.Count == 0;

        private NavigationElementsSet _navigationElementsSet;

        private Stack<NavigationPoint> _navigationChain;
        private Dictionary<NavigationElementType, List<INavigationElement>> _elements;

        public NavigationBlock(NavigationBlockType type, NavigationElementType rootElementType, NavigationElementsSet navigationElementsSet)
        {
            Type = type;
            RootElementType = rootElementType;
            CurrentPoint = null;

            _navigationElementsSet = navigationElementsSet;

            _navigationChain = new Stack<NavigationPoint>();
            _elements = new Dictionary<NavigationElementType, List<INavigationElement>>();
        }

        public void RegisterElement(INavigationElement element)
        {
            foreach (var type in element.Types)
            {
                if (!_elements.ContainsKey(type))
                    _elements.Add(type, new List<INavigationElement>());

                _elements[type].Add(element);
            }
        }

        public bool HandlePointClick(NavigationPoint point)
        {
            if (!point.Element.OnClick(point.Type))
                return false;

            GoToPoint(point, TransitionType.In);

            return true;
        }

        public IEnumerable<NavigationPoint> GetChildPointsOfType(NavigationElementType type)
        {
            if (_navigationElementsSet.TryGetElementSettings(type, out var settings))
                return settings.ChildTypes.SelectMany(GetPointsOfType);

            return Enumerable.Empty<NavigationPoint>();
        }

        public IEnumerable<NavigationPoint> GetPointsOfType(NavigationElementType type)
        {
            if (!_elements.ContainsKey(type))
                return new List<NavigationPoint>();

            return _elements[type]
                .Where(e => e.CanDisplay(type))
                .Select(e => new NavigationPoint(type, e));
        }

        public void ClearNavigationChain()
        {
            _navigationChain.Clear();
            CurrentPoint = null;
        }

        public void GoToRootPoint()
        {
            GoToPoint(GetPointsOfType(RootElementType).FirstOrDefault(), TransitionType.In);
        }

        public void GoToPreviousPoint()
        {
            if (_navigationChain.Count == 0)
                return;

            _navigationChain.Pop();

            if (_navigationChain.Count == 0)
                return;

            GoToPoint(_navigationChain.Pop(), TransitionType.Out);
        }

        private void GoToPoint(NavigationPoint point, TransitionType transitionType)
        {
            if (point == null)
                return;

            CurrentPoint = point;
            _navigationChain.Push(point);
        }
    }


    public enum TransitionType
    {
        In = 0,
        Out = 1
    }
}