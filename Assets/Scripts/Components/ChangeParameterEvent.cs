using Core;

namespace Components
{
    public struct ChangeParameterEvent
    {
        public ParameterChangeRequest Request;
        public string PetId;

        public sealed class ParameterChangeRequest
        {
            public ParameterType ParameterType;
            public float Value;
        }
    }
}