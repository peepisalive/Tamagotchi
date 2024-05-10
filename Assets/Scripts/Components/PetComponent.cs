using Core;

namespace Components
{
    public struct PetComponent
    {
        public Pet Pet { get; private set; }

        public PetComponent(Pet pet)
        {
            Pet = pet;
        }
    }
}