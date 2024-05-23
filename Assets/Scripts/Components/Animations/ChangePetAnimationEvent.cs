using Core.Animation;

namespace Components
{
    public struct ChangePetAnimationEvent
    {
        public AnimationType Type { get; private set; }

        public ChangePetAnimationEvent(AnimationType type)
        {
            Type = type;
        }
    }
}