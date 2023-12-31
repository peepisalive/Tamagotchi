namespace Save
{
    public interface IStateHolder
    {
        public string Id { get; }

        public string StateToString();
        public void RestoreState(string state);
    }


    public interface IStateHolder<out T> : IStateHolder where T : IState
    {
        public T State { get; }
    }
}