using Leopotam.Ecs;
using Components;
using Core;
using Save.State;

namespace System
{
    public sealed class PetCreationSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<SaveDataLoadedComponent> _saveDataFilter;

        public void Init()
        {
            if (_saveDataFilter.IsEmpty())
            {
                CreatePet();
            }
            else
            {
                LoadPet();
            }
        }

        private void CreatePet()
        {
            _world.NewEntity().Replace(new PetComponent
            {
                Pet = new Pet("Frogggg", PetType.Frog, null, Guid.NewGuid().ToString())
            });
        }

        private void LoadPet()
        {
            foreach (var i in _saveDataFilter)
            {
                var saveData = _saveDataFilter.Get1(i);
                var save = saveData.Get<PetStateHolder>().State;

                _world.NewEntity().Replace(new PetComponent
                {
                    Pet = new Pet(save.Name, save.Type, null, save.Id)
                });
            }
        }
    }
}