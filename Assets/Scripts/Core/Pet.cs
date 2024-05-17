using System.Collections.Generic;

namespace Core
{
    public sealed class Pet : BaseObject
    {
        public readonly string Name;
        public readonly PetType Type;
        public readonly Parameters Parameters;
        public readonly List<Accessory> Accessories;

        public Pet(string id) : base(id) { }

        public Pet(string name, PetType type, Parameters parameters, List<Accessory> accessories, string id) : base(id)
        {
            Name = name;
            Type = type;
            Parameters = parameters;
            Accessories = accessories;
        }
    }
}