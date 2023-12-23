namespace Core
{
    public sealed class Pet : BaseObject
    {
        public Parameters Parameters { get; private set; }

        public Pet(string id) : base(id)
        {

        }
    }
}