using System.Collections.Generic;
using Leopotam.Ecs;
using System.Linq;
using Components;
using Save.State;
using Modules;
using Save;

namespace Systems
{
    public sealed class SaveDataSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<SaveDataEvent> _saveDataFilter;

        private EcsFilter<BankAccountComponent> _bankAccountFilter;
        private EcsFilter<PetComponent> _petFilter;
        private EcsFilter<JobComponent> _jobFilter;

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
            SaveJobData();

            _saveDataManager.SaveData(isAsync);
        }

        private void SavePetData()
        {
            foreach (var i in _petFilter)
            {
                var stateHolder = _saveDataManager.GetStateHolder<PetStateHolder>();
                var pet = _petFilter.Get1(i).Pet;

                stateHolder.ResetState();

                stateHolder.State.Id = pet.Id;
                stateHolder.State.Name = pet.Name;
                stateHolder.State.Type = pet.Type;
                stateHolder.State.Parameters = pet.Parameters.GetSaves();
            }
        }

        private void SaveJobData()
        {
            foreach (var i in _jobFilter)
            {
                var stateHolder = _saveDataManager.GetStateHolder<JobStateHolder>();
                var component = _jobFilter.Get1(i);

                stateHolder.ResetState();

                if (component.CurrentJob != null)
                    stateHolder.State.CurrentJob = component.CurrentJob.GetSave() as FullTimeJobSave;

                var jobSaves = new List<JobSave>();

                component.AvailableJob.ToList().ForEach(job =>
                {
                    jobSaves.Add(job.GetSave());
                });
                stateHolder.State.FullTimeJob.AddRange(jobSaves.Where(save => save is FullTimeJobSave).Cast<FullTimeJobSave>());
                stateHolder.State.PartTimeJob.AddRange(jobSaves.Where(save => save is PartTimeJobSave).Cast<PartTimeJobSave>());
            }
        }

        private void SaveSettingsData()
        {
            var stateHolder = _saveDataManager.GetStateHolder<SettingsStateHolder>();

            stateHolder.ResetState();

            SaveSoundData();


            void SaveSoundData()
            {
                stateHolder.State.SoundState = SoundProvider.Instance.State;
            }
        }

        private void SaveGlobalData()
        {
            var stateHolder = _saveDataManager.GetStateHolder<GlobalStateHolder>();

            stateHolder.ResetState();

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