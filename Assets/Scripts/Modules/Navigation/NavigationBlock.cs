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
        public Stack<NavigationPoint> NavigationChain { get; private set; }

        private NavigationElementsSet _navigationElementsSet;
        private Dictionary<NavigationElementType, List<INavigationElement>> _elements;

        public NavigationBlock(NavigationBlockType type, NavigationElementType rootElementType, NavigationElementsSet navigationElementsSet)
        {
            Type = type;
            RootElementType = rootElementType;
            CurrentPoint = null;

            _navigationElementsSet = navigationElementsSet;

            NavigationChain = new Stack<NavigationPoint>();
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

        public NavigationButtonData GetNavigationButtonData(NavigationElementType type, INavigationElement element)
        {
            NavigationButtonData buttonData = null;

            if (_navigationElementsSet.TryGetElementSettings(type, out var settings))
            {
                buttonData = settings.GetNavigationButtonData();
                buttonData.StateType = element.IsEnable(type) switch
                {
                    false => NavigationButtonState.Locked,
                    _ when (settings.ChildTypes.Count == 0) => NavigationButtonState.NonTransition,
                    _ => NavigationButtonState.Transition
                };
            }

            return buttonData;
        }

        public NavigationScreenData GetNavigationScreenData(NavigationElementType type)
        {
            if (_navigationElementsSet.TryGetElementSettings(type, out var settings))
                return settings.GetNavigationScreenData();

            return null;
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
            NavigationChain.Clear();
            CurrentPoint = null;
        }

        public void GoToRootPoint()
        {
            GoToPoint(GetPointsOfType(RootElementType).FirstOrDefault(), TransitionType.In);
        }

        public void GoToPreviousPoint()
        {
            if (NavigationChain.Count == 0)
                return;

            NavigationChain.Pop();

            if (NavigationChain.Count == 0)
                return;

            GoToPoint(NavigationChain.Pop(), TransitionType.Out);
        }

        private void GoToPoint(NavigationPoint point, TransitionType transitionType)
        {
            if (point == null)
                return;

            CurrentPoint = point;
            NavigationChain.Push(point);
        }
    }
}