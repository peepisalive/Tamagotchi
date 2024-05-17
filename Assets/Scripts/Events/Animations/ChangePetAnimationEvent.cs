using Core.Animation;

namespace Events
{
    public sealed class ChangePetAnimationEvent
    {
        public AnimationType Type { get; private set; }

        public ChangePetAnimationEvent(AnimationType type)
        {
            Type = type;
        }
    }
}