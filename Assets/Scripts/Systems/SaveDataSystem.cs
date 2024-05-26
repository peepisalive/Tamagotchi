using System.Collections.Generic;
using Modules.Localization;
using Leopotam.Ecs;
using System.Linq;
using Components;
using Save.State;
using Modules;
using System;
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
                var IsDeath = _petFilter.GetEntity(i).Has<DeadComponent>();
                var pet = _petFilter.Get1(i).Pet;

                stateHolder.ResetState();

                stateHolder.State.Id = pet.Id;
                stateHolder.State.Name = pet.Name;
                stateHolder.State.Type = pet.Type;
                stateHolder.State.IsDeath = IsDeath;
                stateHolder.State.Parameters = pet.Parameters.GetSaves();
                stateHolder.State.Accessories = new List<AccessorySave>();

                pet.Accessories.ForEach(accessory =>
                {
                    stateHolder.State.Accessories.Add(new AccessorySave
                    {
                        Type = accessory.Type,
                        Color = new ColorSave(accessory.Color),
                        IsCurrent = accessory.IsCurrent,
                        IsUnlocked = accessory.IsUnlocked
                    });
                });
            }
        }

        private void SaveJobData()
        {
            foreach (var i in _jobFilter)
            {
                var stateHolder = _saveDataManager.GetStateHolder<JobStateHolder>();
                var component = _jobFilter.Get1(i);

                stateHolder.ResetState();

                if (component.CurrentFullTimeJob != null)
                    stateHolder.State.CurrentFullTimeJob = component.CurrentFullTimeJob.GetSave();

                var jobSaves = new List<JobSave>();

                component.AvailableJob.ToList().ForEach(job =>
                {
                    jobSaves.Add(job.GetSave());
                });

                stateHolder.State.FullTimeJob.AddRange(jobSaves.Where(save => save is FullTimeJobSave).Cast<FullTimeJobSave>());
                stateHolder.State.PartTimeJob.AddRange(jobSaves.Where(save => save is PartTimeJobSave).Cast<PartTimeJobSave>());

                stateHolder.State.PartTimeJobAmountPerDay = component.PartTimeAmountPerDay;
                stateHolder.State.StartPartTimeJobRecovery = component.StartPartTimeRecovery;
            }
        }

        private void SaveSettingsData()
        {
            var stateHolder = _saveDataManager.GetStateHolder<SettingsStateHolder>();

            stateHolder.ResetState();

            stateHolder.State.SoundState = SoundProvider.Instance.State;
            stateHolder.State.HapticState = HapticProvider.Instance.State;
            stateHolder.State.Language = LocalizationProvider.CurrentLanguage;
        }

        private void SaveGlobalData()
        {
            var stateHolder = _saveDataManager.GetStateHolder<GlobalStateHolder>();

            SaveBankAccountData();
            SavePlayTimeData();
            SaveExitDate();

            void SavePlayTimeData()
            {
                stateHolder.State.TotalPlayTimeSeconds = InGameTimeManager.Instance.TotalPlayTimeSeconds;
                stateHolder.State.LastSessionPlayTimeSeconds = InGameTimeManager.Instance.LastSessionPlayTimeSeconds;
            }
            void SaveBankAccountData()
            {
                foreach (var i in _bankAccountFilter)
                {
                    stateHolder.State.BankAccountValue = _bankAccountFilter.Get1(i).BankAccount.Value;
                }
            }
            void SaveExitDate()
            {
                var currentDate = DateTime.Now;
                var lastSessionPlayTimeSeconds = InGameTimeManager.Instance.TotalPlayTimeSeconds;

                stateHolder.State.LastExitDate = currentDate;
                stateHolder.State.LastLaunchDate = currentDate - TimeSpan.FromSeconds(lastSessionPlayTimeSeconds);
            }
        }
    }
}