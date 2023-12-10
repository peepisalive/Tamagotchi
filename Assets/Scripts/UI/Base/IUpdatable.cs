namespace UI
{
    public interface IUpdatable<T> where T : class
    {
        public void UpdateState(T data);
    }
}