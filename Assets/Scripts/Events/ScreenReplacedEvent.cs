using UI.Screen.Controller;

namespace Events
{
    public sealed class ScreenReplacedEvent
    {
        public ScreenController CurrentScreen;
        public bool FadeOffRequired;
    }
}