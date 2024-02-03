using System;

namespace Core
{
    public sealed class BankAccount
    {
        public int Value { get; private set; }
        public event Action<int> OnValueChangedEvent;

        public BankAccount(int value)
        {
            Value = value;
        }

        public void Add(int value)
        {
            Value += value;
            OnValueChangedEvent?.Invoke(Value);
        }
    }
}