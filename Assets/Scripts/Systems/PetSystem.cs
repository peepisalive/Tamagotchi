using Leopotam.Ecs;
using System.Linq;
using Save.State;
using Components;
using Settings;
using Modules;
using System;
using Core;

namespace Systems
{
    public sealed class PetSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter<PetComponent> _petFilter;
        private EcsFilter<SaveDataLoadedComponent> _saveDataFilter;
        private EcsFilter<ChangePetAnimationEvent> _petAnimationFilter;
        private EcsFilter<ChangePetEyesAnimationEvent> _petEyesAnimationFilter;

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

            EventSystem.Subscribe<Events.ChangePetEyesAnimationEvent>(SendComponent);
            EventSystem.Subscribe<Events.ChangePetAnimationEvent>(SendComponent);
        }

        public void Run()
        {
            if (!_petEyesAnimationFilter.IsEmpty())
            {
                foreach (var i in _petEyesAnimationFilter)
                {
                    var eyesAnimation = _petEyesAnimationFilter.Get1(i).Type;

                    foreach (var j in _petFilter)
                    {
                        _petFilter.Get1(i).Pet.SetEyesAnimation(eyesAnimation);
                    }
                }
            }

            if (!_petAnimationFilter.IsEmpty())
            {
                foreach (var i in _petAnimationFilter)
                {
                    var animation = _petAnimationFilter.Get1(i).Type;

                    foreach (var j in _petFilter)
                    {
                        _petFilter.Get1(i).Pet.SetAnimation(animation);
                    }
                }
            }
        }

        public void Destroy()
        {
            EventSystem.Unsubscribe<Events.ChangePetEyesAnimationEvent>(SendComponent);
            EventSystem.Unsubscribe<Events.ChangePetAnimationEvent>(SendComponent);
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
            var pet = new Pet("Eva", PetType.Lamb, parameters, accessories, Guid.NewGuid().ToString());

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
                var entity = _world.NewEntity().Replace(new PetComponent(pet));

                if (!save.IsDeath)
                    continue;

                entity.Replace(new DeathEvent());
            }
        }

        private void SendComponent(Events.ChangePetEyesAnimationEvent e)
        {
            _world.NewEntity().Replace(new ChangePetEyesAnimationEvent(e.Type));
        }

        private void SendComponent(Events.ChangePetAnimationEvent e)
        {
            _world.NewEntity().Replace(new ChangePetAnimationEvent(e.Type));
        }
    }
}