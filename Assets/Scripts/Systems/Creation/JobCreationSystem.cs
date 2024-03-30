using Leopotam.Ecs;
using Components;
using Core.Job;

namespace Systems.Creation
{
    public sealed class JobCreationSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<SaveDataLoadedComponent> _saveDataFilter;

        private JobFactory _factory;

        public void Init()
        {
            if (_saveDataFilter.IsEmpty())
            {
                CreateJob();
            }
            else
            {
                LoadJob();
            }
        }

        private void CreateJob()
        {

        }

        private void LoadJob()
        {

        }
    }
}