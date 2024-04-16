using Newtonsoft.Json;

namespace Save.State
{
    public class StateHolder { }


    public class StateHolder<T> : StateHolder, IStateHolder<T> where T : IState, new()
    {
        public T State { get; set; }

        public virtual string Id => string.Empty;

        public StateHolder()
        {
            ResetState();
        }

        public void ResetState()
        {
            State = new T();
        }

        public string StateToString()
        {
            return State.ToString();
        }

        public virtual void RestoreState(string state)
        {
            State = JsonConvert.DeserializeObject<T>(state);
        }
    }
}