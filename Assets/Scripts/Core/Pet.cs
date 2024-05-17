using System.Collections.Generic;
using Core.Animation;

namespace Core
{
    public sealed class Pet : BaseObject
    {
        public EyesAnimationType EyesAnimationType { get; private set; }
        public AnimationType AnimationType { get; private set; }

        public List<Accessory> Accessories { get; private set; }

        public readonly string Name;
        public readonly PetType Type;
        public readonly Parameters Parameters;

        public Pet(string id) : base(id) { }

        public Pet(string name, PetType type, Parameters parameters, string id) : base(id)
        {
            Name = name;
            Type = type;
            Parameters = parameters;
        }

        public void AddAccessory(Accessory accessory)
        {
            Accessories ??= new List<Accessory>();
            Accessories.Add(accessory);
        }
    }
}