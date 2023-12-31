using Newtonsoft.Json;

namespace Save
{
    public class StateHolder { }


    public class StateHolder<T> : StateHolder, IStateHolder<T> where T : IState, new()
    {
        public T State { get; set; }

        public virtual string Id => string.Empty;

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