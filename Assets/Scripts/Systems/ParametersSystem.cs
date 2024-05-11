using Leopotam.Ecs;
using Components;
using Modules;

namespace Systems
{
    public sealed class ParametersSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter<PetComponent> _petsFilter;
        private EcsFilter<ChangeParameterEvent> _changeParameterFilter;

        public void Init()
        {
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
    }
}