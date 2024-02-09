using Leopotam.Ecs;
using Components;

namespace Utils
{
    public static class GameUtils
    {
        public static bool IsMoneyAvailable(this EcsFilter<BankAccountComponent> bankAccountFilter, int value)
        {
            foreach (var i in bankAccountFilter)
            {
                return bankAccountFilter.Get1(i).BankAccount.IsMoneyAvailable(value);
            }

            return false;
        }
    }
}