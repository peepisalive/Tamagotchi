namespace UI
{
    public interface IStateSettable
    {
        public bool CurrentState { get; }

        public void SetState(bool state);
    }
}