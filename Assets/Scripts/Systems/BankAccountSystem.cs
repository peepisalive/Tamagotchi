using Leopotam.Ecs;
using Components;
using Save.State;
using Modules;
using Core;

namespace Systems
{
    public sealed class BankAccountSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter<SaveDataLoadedComponent> _saveDataFilter;
        private EcsFilter<BankAccountComponent> _bankAccountFilter;
        private EcsFilter<ChangeBankAccountValueEvent> _changeValueFilter;

        public void Init()
        {
            var value = 1703;
            var previousValue = 0;

            if (!_saveDataFilter.IsEmpty())
            {
                foreach (var i in _saveDataFilter)
                {
                    var save = _saveDataFilter.Get1(i).Get<GlobalStateHolder>().State.BankAccount;

                    value = save.Value;
                    previousValue = value;
                }
            }

            CreateBankAccount(previousValue, value);
            EventSystem.Subscribe<Events.ChangeBankAccountValueEvent>(SendEvent);
        }

        public void Run()
        {
            if (_changeValueFilter.IsEmpty())
                return;

            foreach (var i in _changeValueFilter)
            {
                var value = _changeValueFilter.Get1(i).Value;

                foreach (var j in _bankAccountFilter)
                {
                    _bankAccountFilter.Get1(j).BankAccount.Add(value);
                }
            }
        }

        public void Destroy()
        {
            EventSystem.Unsubscribe<Events.ChangeBankAccountValueEvent>(SendEvent);
        }

        private void CreateBankAccount(int previousValue, int value)
        {
            _world.NewEntity().Replace(new BankAccountComponent
            {
                BankAccount = new BankAccount(previousValue, value)
            });
        }

        private void SendEvent(Events.ChangeBankAccountValueEvent e)
        {
            _world.NewEntity().Replace(new ChangeBankAccountValueEvent
            {
                Value = e.Value,
            });
        }
    }
}