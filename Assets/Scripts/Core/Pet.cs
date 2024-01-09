namespace Core
{
    public sealed class Pet : BaseObject
    {
        public string Name { get; private set; }
        public PetType Type { get; private set; }
        public Parameters Parameters { get; private set; }

        public Pet(string id) : base(id) { }

        public Pet(string name, PetType type, Parameters parameters, string id) : base(id)
        {
            Name = name;
            Type = type;
            Parameters = parameters;
        }
    }
}