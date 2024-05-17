using Leopotam.Ecs;
using System.Linq;
using Save.State;
using Components;
using Settings;
using System;
using Core;

namespace Systems.Creation
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

            foreach (var parameterType in Enum.GetValues(typeof(ParameterType)).OfType<ParameterType>())
            {
                parameters.Add(parameterType, new Parameter(parameterType != ParameterType.Fatigue
                    ? valueRange.Max
                    : valueRange.Min, valueRange));
            }

            var accessoriesSettings = SettingsProvider.Get<AccessoriesSettings>();
            var accessories = accessoriesSettings.Accessories.Select(accessorySettings =>
            {
                var accessory = new Accessory
                (
                    accessorySettings.Type,
                    accessorySettings.AccessType,
                    accessorySettings.Value
                );

                if (accessorySettings.Type == AccessoryType.None)
                {
                    accessory.SetUnlockState(true);
                    accessory.SetCurrentState(true);
                }

                return accessory;
            }).ToList();
            var pet = new Pet("Goose", PetType.Goose, parameters, accessories, Guid.NewGuid().ToString());

            _world.NewEntity().Replace(new PetComponent(pet));
        }

        private void LoadPet()
        {
            var accessoriesSettings = SettingsProvider.Get<AccessoriesSettings>();

            foreach (var i in _saveDataFilter)
            {
                var saveData = _saveDataFilter.Get1(i);
                var save = saveData.Get<PetStateHolder>().State;
                var accessories = save.Accessories.Select(accessorySave =>
                {
                    var accessorySettings = accessoriesSettings.GetAccessory(accessorySave.Type);
                    var accessory = new Accessory
                    (
                        accessorySettings.Type,
                        accessorySettings.AccessType,
                        accessorySettings.Value
                    );

                    accessory.SetUnlockState(accessorySave.IsUnlocked);
                    accessory.SetCurrentState(accessorySave.IsCurrent);
                    accessory.SetColor(accessorySave.Color.GetColor());

                    return accessory;
                }).ToList();
                var pet = new Pet(save.Name, save.Type, new Parameters(save.Parameters), accessories, save.Id);

                _world.NewEntity().Replace(new PetComponent(pet));
            }
        }
    }
}