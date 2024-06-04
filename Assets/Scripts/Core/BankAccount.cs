using System;

namespace Core
{
    public sealed class BankAccount
    {
        public event Action<int, int> OnValueChangedEvent;

        public int Value { get; private set; }
        public int PreviousValue { get; private set; }

        public BankAccount(int previousValue, int value)
        {
            Value = value;
            PreviousValue = previousValue;
        }

        public void Add(int value)
        {
            PreviousValue = Value;
            Value += value;

            if (Value < 0)
                Value = 0;

            OnValueChangedEvent?.Invoke(PreviousValue, Value);
        }

        public bool IsMoneyAvailable(int value)
        {
            return Value >= -value;
        }
    }
}