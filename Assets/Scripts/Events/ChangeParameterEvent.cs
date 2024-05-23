using Core;

namespace Events
{
    public sealed class ChangeParameterEvent
    {
        public ParameterType Type;
        public float Value;
    }
}