using Core;

namespace Events
{
    public sealed class ChangeParametersEvent
    {
        public ParameterType Type;
        public float Value;
    }
}