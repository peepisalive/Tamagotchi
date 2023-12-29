namespace Core
{
    public sealed class Pet : BaseObject
    {
        public PetType Type { get; private set; }
        public Parameters Parameters { get; private set; }

        public Pet(string id) : base(id) { }

        public Pet(PetType type, Parameters parameters, string id) : base(id)
        {
            Type = type;
            Parameters = parameters;
        }
    }
}