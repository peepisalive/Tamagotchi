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

            if (Value < 0)
                Value = 0;

            OnValueChangedEvent?.Invoke(Value);
        }

        public bool IsMoneyAvailable(int value)
        {
            return Value >= -value;
        }
    }
}