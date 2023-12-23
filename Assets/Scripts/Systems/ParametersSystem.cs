using Leopotam.Ecs;
using Components;

namespace Systems
{
    public sealed class ParametersSystem : IEcsRunSystem
    {
        private EcsFilter<PetComponent> _petsFilter;
        private EcsFilter<ChangeParameterEvent> _changeParameterFilter;

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

                        if (pet.Id != comp.PetId)
                            continue;

                        var request = comp.Request;
                        var parameter = pet.Parameters.Get(request.ParameterType);

                        parameter.Add(comp.Request.Value);
                    }
                }
            }
        }
    }
}