using Leopotam.Ecs;
using Components;
using Save.State;
using Modules;

namespace Systems
{
    public sealed class SaveDataSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<SaveDataEvent> _saveDataFilter;

        private EcsFilter<BankAccountComponent> _bankAccountFilter;
        private EcsFilter<PetComponent> _petFilter;

        private SaveDataManager _saveDataManager;

        public void Init()
        {
            _saveDataManager.TryLoadData();
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
            SaveSettingsData();
            SaveGlobalData();
            SavePetData();

            _saveDataManager.SaveData(isAsync);
        }

        private void SavePetData()
        {
            foreach (var i in _petFilter)
            {
                var stateHolder = _saveDataManager.GetStateHolder<PetStateHolder>();
                var pet = _petFilter.Get1(i).Pet;

                stateHolder.State.Id = pet.Id;
                stateHolder.State.Name = pet.Name;
                stateHolder.State.Type = pet.Type;
                stateHolder.State.Parameters = pet.Parameters.GetSaves();
            }
        }

        private void SaveSettingsData()
        {
            var stateHolder = _saveDataManager.GetStateHolder<SettingsStateHolder>();

            SaveSoundData();


            void SaveSoundData()
            {
                stateHolder.State.SoundState = SoundProvider.Instance.State;
            }
        }

        private void SaveGlobalData()
        {
            var stateHolder = _saveDataManager.GetStateHolder<GlobalStateHolder>();

            SaveBankAccountData();
            SavePlayTimeData();


            void SavePlayTimeData()
            {
                stateHolder.State.PlayTimeSeconds = InGameTimeManager.Instance.PlayTimeSeconds;
            }
            void SaveBankAccountData()
            {
                foreach (var i in _bankAccountFilter)
                {
                    stateHolder.State.BankAccountValue = _bankAccountFilter.Get1(i).BankAccount.Value;
                }
            }
        }
    }
}