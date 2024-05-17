using Core.Animation;

namespace Components
{
    public struct ChangePetEyesAnimationEvent
    {
        public EyesAnimationType Type { get; private set; }

        public ChangePetEyesAnimationEvent(EyesAnimationType type)
        {
            Type = type;
        }
    }
}