using Core.Animation;

namespace Events
{
    public sealed class ChangePetEyesAnimationEvent
    {
        public EyesAnimationType Type { get; private set; }

        public ChangePetEyesAnimationEvent(EyesAnimationType type)
        {
            Type = type;
        }
    }
}