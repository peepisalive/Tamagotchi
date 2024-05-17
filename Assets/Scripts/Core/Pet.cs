using System.Collections.Generic;
using Core.Animation;

namespace Core
{
    public sealed class Pet : BaseObject
    {
        public EyesAnimationType EyesAnimationType { get; private set; }
        public AnimationType AnimationType { get; private set; }

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