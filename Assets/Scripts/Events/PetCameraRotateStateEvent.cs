namespace Events
{
    public sealed class PetCameraRotateStateEvent
    {
        public bool State { get; private set; }

        public PetCameraRotateStateEvent(bool state)
        {
            State = state;
        }
    }
}