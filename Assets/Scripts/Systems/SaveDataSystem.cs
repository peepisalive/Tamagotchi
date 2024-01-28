using Leopotam.Ecs;
using Components;
using Save.State;
using Modules;

namespace System
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
            SaveDataBankAccount();
            SaveDataPet();

            _saveDataManager.SaveData(isAsync);
        }

        private void SaveDataBankAccount()
        {
            foreach (var i in _bankAccountFilter)
            {
                var stateHolder = _saveDataManager.GetStateHolder<GlobalStateHolder>();
                var bankAccount = _bankAccountFilter.Get1(i).BankAccount;

                stateHolder.State.BankAccountValue = bankAccount.Value;
            }
        }

        private void SaveDataPet()
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
    }
}