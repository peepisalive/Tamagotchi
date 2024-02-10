using Leopotam.Ecs;
using Save.State;
using Components;
using Core;

namespace System.Creation
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
            var parameters = new Parameters();
            var valueRange = new FloatRange(0f, 1f);

            parameters.Add(ParameterType.Happiness, new Parameter(valueRange.Max, valueRange));
            parameters.Add(ParameterType.Satiety, new Parameter(valueRange.Max, valueRange));
            parameters.Add(ParameterType.Hygiene, new Parameter(valueRange.Max, valueRange));
            parameters.Add(ParameterType.Fatigue, new Parameter(valueRange.Max, valueRange));
            parameters.Add(ParameterType.Health, new Parameter(valueRange.Max, valueRange));

            _world.NewEntity().Replace(new PetComponent
            {
                Pet = new Pet("Frogggg", PetType.Frog, parameters, Guid.NewGuid().ToString())
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
                    Pet = new Pet(save.Name, save.Type, new Parameters(save.Parameters), save.Id)
                });
            }
        }
    }
}