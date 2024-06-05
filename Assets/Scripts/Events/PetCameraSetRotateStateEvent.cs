namespace Events
{
    public sealed class PetCameraSetRotateStateEvent
    {
        public bool State { get; private set; }

        public PetCameraSetRotateStateEvent(bool state)
        {
            State = state;
        }
    }
}