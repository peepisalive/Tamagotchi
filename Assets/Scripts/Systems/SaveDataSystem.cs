using Leopotam.Ecs;
using Components;
using Tamagotchi;

namespace System
{
    public sealed class SaveDataSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<SaveDataEvent> _saveDataFilter;

        public void Init()
        {
            Application.SaveDataManager.TryLoadData();
        }

        public void Run()
        {
            if (_saveDataFilter.IsEmpty())
                return;

            foreach (var i in _saveDataFilter)
            {
                SaveData(_saveDataFilter.Get1(i).IsAsync);
            }
        }

        private void SaveData(bool isAsync)
        {
            Application.SaveDataManager.SaveData(isAsync);
        }
    }
}