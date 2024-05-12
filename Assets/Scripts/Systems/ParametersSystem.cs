using Leopotam.Ecs;
using Save.State;
using Components;
using Settings;
using Modules;
using System;

namespace Systems
{
    public sealed class ParametersSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter<PetComponent> _petsFilter;
        private EcsFilter<SaveDataLoadedComponent> _saveDataFilter;
        private EcsFilter<ChangeParameterEvent> _changeParameterFilter;

        private ParametersSettings _settings;

        public void Init()
        {
            _settings = SettingsProvider.Get<ParametersSettings>();

            if (!_saveDataFilter.IsEmpty())
                ChangeParameterByTime();

            EventSystem.Subscribe<Events.ChangeParameterEvent>(ChangeParameters);
        }

        public void Run()
        {
            if (!_changeParameterFilter.IsEmpty())
            {
                foreach (var i in _changeParameterFilter)
                {
                    var comp = _changeParameterFilter.Get1(i);

                    foreach (var j in _petsFilter)
                    {
                        var pet = _petsFilter.Get1(j).Pet;
                        var parameter = pet.Parameters.Get(comp.Type);

                        parameter.Add(comp.Value);
                    }
                }
            }
        }

        public void Destroy()
        {
            EventSystem.Unsubscribe<Events.ChangeParameterEvent>(ChangeParameters);
        }

        private void ChangeParameters(Events.ChangeParameterEvent e)
        {
            _world.NewEntity().Replace(new ChangeParameterEvent
            {
                Type = e.Type,
                Value = e.Value
            });
        }

        private void ChangeParameterByTime()
        {
            foreach (var i in _saveDataFilter)
            {
                var currentDate = DateTime.Now;
                var saveData = _saveDataFilter.Get1(i);
                var save = saveData.Get<GlobalStateHolder>().State;

                var multiplier = (float)(currentDate - save.LastExitDate).TotalSeconds / _settings.ChangeTimeInSeconds;

                if (multiplier < 1f)
                    return;

                _settings.ParameterDecRanges.ForEach(parameter =>
                {
                    _world.NewEntity().Replace(new ChangeParameterEvent
                    {
                        Type = parameter.Type,
                        Value = parameter.Range.GetRandom() * multiplier
                    });
                });
            }
        }
    }
}